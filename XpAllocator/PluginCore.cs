using Decal.Adapter;
using Decal.Adapter.Wrappers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace XpAllocator
{
    /// <summary>
    /// This is the main plugin class. When your plugin is loaded, Startup() is called, and when it's unloaded Shutdown() is called.
    /// </summary>
    [FriendlyName("XpAllocator")]
    public class PluginCore : PluginBase
    {
        private ExampleUI ui;
        private readonly Regex SkillRaiseRegex = new Regex(@"^Your .* is now \d+");



        /// <summary>
        /// Assembly directory containing the plugin dll
        /// </summary>
        public static string AssemblyDirectory => System.IO.Path.GetDirectoryName(Assembly.GetAssembly(typeof(PluginCore)).Location);

        /// <summary>
        /// Called when your plugin is first loaded.
        /// </summary>
        protected override void Startup()
        {
            try
            {
                Globals.Init("XpAllocator", CoreManager.Current);

                var isHotReload = CoreManager.Current.CharacterFilter.LoginStatus == 3;
                if (isHotReload)
                {
                    Setup();
                }
                else
                {
                    // subscribe to CharacterFilter_LoginComplete event, make sure to unscribe later.
                    // note: if the plugin was reloaded while ingame, this event will never trigger on the newly reloaded instance.
                    CoreManager.Current.CharacterFilter.LoginComplete += CharacterFilter_LoginComplete;
                }

                ui = new ExampleUI();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        /// <summary>
        /// CharacterFilter_LoginComplete event handler.
        /// </summary>
        private void CharacterFilter_LoginComplete(object sender, EventArgs e)
        {
            // it's generally a good idea to use try/catch blocks inside of decal event handlers.
            // throwing an uncaught exception inside one will generally hard crash the client.
            try
            {
                Setup();
                CoreManager.Current.CharacterFilter.LoginComplete -= CharacterFilter_LoginComplete;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void Setup()
        {
            Globals.Core.CharacterFilter.ChangeExperience += ExperienceFilter_Earned;
            Globals.Core.ChatBoxMessage += ContinueOnSkillRaised;
            Globals.Core.CommandLineText += Core_CommandLineText;
            Globals.XpAllocator = new XpAllocator(LoadConfig());
            //Globals.XpAllocator.AllocateXp();
        }

        /// <summary>
        /// Called when your plugin is unloaded. Either when logging out, closing the client, or hot reloading.
        /// </summary>
        protected override void Shutdown()
        {
            try
            {
                // make sure to unsubscribe from any events we were subscribed to. Not doing so
                // can cause the old plugin to stay loaded between hot reloads.
                Globals.Core.CharacterFilter.ChangeExperience -= ExperienceFilter_Earned;
                Globals.Core.ChatBoxMessage -= ContinueOnSkillRaised;
                Globals.Core.CommandLineText -= Core_CommandLineText;

                // clean up our ui view
                ui.Dispose();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void Core_CommandLineText(object sender, ChatParserInterceptEventArgs e)
        {
            var command = e.Text.ToLower();
            if (command.StartsWith("/xpa ") || command.StartsWith("@xpa ") || command == "/xpa")
            {
                e.Eat = true;

                var parts = command.Split(' ');
                var verb = parts.Length > 1 ? parts[1] : null;

                if (verb == "save") SaveConfig();
                if (verb == "enable") Globals.Config.Enabled = true;
                if (verb == "disable") Globals.Config.Enabled = false;
                //if (verb == "reserve") Globals.Config.Reserve = parts[2];
                if (verb == "set")
                {
                    if (parts.Length < 4) Util.WriteToChat($"Correct usage is /xpa set <skill> <weight> (i.e. /xpa set run .05");
                    if (!int.TryParse(parts[3], out var weight)) Util.WriteToChat($"{parts[3]} is not a valid weight");

                    Globals.Config.SetWeight(parts[2], weight);
                    Globals.XpAllocator = new XpAllocator(Globals.Config);
                }
                if (verb == "weights")
                {
                    Util.WriteToChat($"Current Weightings: \n{Globals.XpAllocator.Weights()}");
                }
                if (verb == "reset")
                {
                    Globals.XpAllocator = new XpAllocator(LoadConfig());
                    Util.WriteToChat("Reset!");
                }
            }
        }

        private void ContinueOnSkillRaised(object sender, ChatTextInterceptEventArgs e)
        {
            if (e.Color == 13 && SkillRaiseRegex.IsMatch(e.Text))
            {
                Globals.XpAllocator.AllocateXp(skillRaiseSuccess: true);
            }
        }

        void ExperienceFilter_Earned(object sender, ChangeExperienceEventArgs e)
        {
            // Handle stuff when he have a change in unassigned XP
            if (e.Type == PlayerXPEventType.Unassigned && e.Amount > 0)
            {
                Globals.XpAllocator.AllocateXp();
            }
        }

        private PlayerConfiguration LoadConfig()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = $"{appdata}\\VehnPlugins\\XpAllocator\\{Core.CharacterFilter.Server}\\{Core.CharacterFilter.AccountName}\\{Core.CharacterFilter.Name}.json";

            if (!File.Exists(configPath))
            {
                Globals.Config = PlayerConfiguration.Defaults();
            }
            else
            {
                var file = new StreamReader(configPath);

                var preferencesJson = file.ReadToEnd();
                file.Close();
                Globals.Config = JsonConvert.DeserializeObject<PlayerConfiguration>(preferencesJson);
            }

            return Globals.Config;
        }

        private void SaveConfig()
        {
            var appdata = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var configPath = $"{appdata}\\VehnPlugins\\XpAllocator\\{Core.CharacterFilter.Server}\\{Core.CharacterFilter.AccountName}\\{Core.CharacterFilter.Name}.json";

            if (!File.Exists(configPath))
                CreateChildDirectories(configPath);

            var file = new StreamWriter(configPath, false);

            file.Write(JsonConvert.SerializeObject(Globals.Config));
            file.Close();
        }

        private void CreateChildDirectories(string preferencesFilePath)
        {
            var lastSlash = preferencesFilePath.LastIndexOf('\\');
            var directory = preferencesFilePath.Substring(0, lastSlash);
            if (!Directory.Exists(directory))
            {
                CreateChildDirectories(directory);
                Directory.CreateDirectory(directory);
            }
        }


        #region logging
        /// <summary>
        /// Log an exception to log.txt in the same directory as the plugin.
        /// </summary>
        /// <param name="ex"></param>
        internal static void Log(Exception ex)
        {
            Log(ex.ToString());
            Util.WriteToChat(ex.ToString());
        }

        /// <summary>
        /// Log a string to log.txt in the same directory as the plugin.
        /// </summary>
        /// <param name="message"></param>
        internal static void Log(string message)
        {
            try
            {
                File.AppendAllText(System.IO.Path.Combine(AssemblyDirectory, "log.txt"), $"{message}\n");
            }
            catch { }
        }
        #endregion // logging
    }
}

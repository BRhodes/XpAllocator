using Decal.Adapter;
using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Numerics;
using UtilityBelt.Service;
using UtilityBelt.Service.Views;

namespace XpAllocator
{
    internal class ExampleUI : IDisposable
    {
        /// <summary>
        /// The UBService Hud
        /// </summary>
        private readonly Hud hud;

        /// <summary>
        /// The default value for TestText.
        /// </summary>
        public const string DefaultTestText = "Some Test Text";

        /// <summary>
        /// Some test text. This value is used to the text input in our UI.
        /// </summary>
        public string TestText = DefaultTestText.ToString();

        public bool enabled = default;
        public bool attributeBasedWeights = true;
        public string reserve = "0";
        public string reserveMax = "0";
        public string reservePercent = "0%";

        public ExampleUI()
        {
            // Create a new UBService Hud
            hud = UBService.Huds.CreateHud("XpAllocator");

            // set to show our icon in the UBService HudBar
            hud.ShowInBar = true;

            // subscribe to the hud render event so we can draw some controls
            hud.OnRender += Hud_OnRender;
        }
        double a = 10.0;
        double b = 5.0;
        int c = 0;
        string d = "";
        /// <summary>
        /// Called every time the ui is redrawing.
        /// </summary>
        static int x = 0; 

        int[] ints = { 1, 2, 3, 4, 5, 6 };
        List<int> intss = new List<int>() { 1, 2, 3, 4, 5, 6 };

        private void Hud_OnRender(object sender, EventArgs e)
        {
            try
            {
                if (Globals.Config == null) return;
                bool enabled = Globals.Config.Enabled;

                ImGui.BeginTable("TopLevelConfig", 2);
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Checkbox("enabled", ref enabled);
                ImGui.TableSetColumnIndex(1);
                ImGui.InputDouble("res. %", ref a);
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Checkbox("auto att. wt.", ref enabled);
                ImGui.TableSetColumnIndex(1);
                ImGui.InputText("reserve", ref d, 50);
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(1);
                ImGui.InputText("max res.", ref d, 50);

                ImGui.EndTable();
                if (ImGui.CollapsingHeader("Attributes"))
                {
                    ImGui.BeginTable("aa#att", 4);
                    ImGui.TableNextRow();
                    ImGui.TableSetColumnIndex(0);
                    ImGui.Text("Attribute");
                    ImGui.TableSetColumnIndex(1);
                    ImGui.Text($"Config Weight");
                    ImGui.TableSetColumnIndex(2);
                    ImGui.Text($"Eff. Weight");
                    ImGui.TableSetColumnIndex(3);
                    ImGui.Text($"Total XP");


                    steve("strength", ref ints[0]);
                    steve("endurance", ref intss[1]);
                    steve("coordination", ref intss[2]);
                    steve("quickness", ref ints[3]);
                    steve("focus", ref ints[4]);
                    steve("self", ref ints[5]);





                    ImGui.EndTable();
                }
                ImGui.CollapsingHeader("Vitals");
                ImGui.CollapsingHeader("Skills");
              
                ImGui.InputTextMultiline("b#Test Txxt", ref TestText, 5000, new Vector2(400, 150));

                if (ImGui.Button("b#Print Test Text"))
                {
                    OnPrintTestTextButtonPressed();
                }

                ImGui.SameLine();

                if (ImGui.Button("b#Reset Test Text"))
                {
                    TestText = DefaultTestText;
                }
                ImGui.EndTabItem();
                ImGui.EndTabBar();
            }
            catch (Exception ex)
            {
                PluginCore.Log(ex);
            }
        }

        void steve(string label, ref int x)
        {
            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            ImGui.Text(label);
            ImGui.TableSetColumnIndex(1);
            ImGui.InputInt("##" + label , ref x);
            ImGui.TableSetColumnIndex(2);
            ImGui.Text($"1");
            ImGui.TableSetColumnIndex(3);
            ImGui.Text($"0");
        }

        /// <summary>
        /// Called when our print test text button is pressed
        /// </summary>
        private void OnPrintTestTextButtonPressed()
        {
            var textToShow = $"Test Text:\n{TestText}";

            CoreManager.Current.Actions.AddChatText(textToShow, 1);
            UBService.Huds.Toaster.Add(textToShow, ToastType.Info);
        }

        public void Dispose()
        {
            hud.Dispose();
        }
    }
}
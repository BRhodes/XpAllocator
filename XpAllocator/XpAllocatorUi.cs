using Decal.Adapter;
using ImGuiNET;
using System;
using UtilityBelt.Service;
using UtilityBelt.Service.Views;

namespace XpAllocator
{
    internal class XpAllocatorUi : IDisposable
    {
        /// <summary>
        /// The UBService Hud
        /// </summary>
        private readonly Hud hud;

        public XpAllocatorUi()
        {
            // Create a new UBService Hud
            hud = UBService.Huds.CreateHud("XpAllocator");

            // set to show our icon in the UBService HudBar
            hud.ShowInBar = true;

            // subscribe to the hud render event so we can draw some controls
            hud.OnRender += Hud_OnRender;
        }

        /// <summary>
        /// Called every time the ui is redrawing.
        /// </summary>
        private void Hud_OnRender(object sender, EventArgs e)
        {
            try
            {
                if (Globals.Config == null) return;

                // size/position the window (or save that info)
                if (Globals.Config.PositionSet)
                {
                    Globals.Config.Size = ImGui.GetWindowSize();
                    Globals.Config.Pos = ImGui.GetWindowPos();
                }
                else
                {
                    ImGui.SetWindowSize(Globals.Config.Size);
                    ImGui.SetWindowPos(Globals.Config.Pos);
                    Globals.Config.PositionSet = true;
                }

                ImGui.BeginTable("TopLevelConfig", 2);
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                if (ImGui.Button("Reset"))
                {
                    Globals.XpAllocator.Reset();
                    Globals.XpAllocator.AllocateXp();
                    Util.WriteToChat("Reset!");
                }
                ImGui.TableSetColumnIndex(1);
                ImGui.InputInt("res. %", ref Globals.Config.ReservePercent);
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Checkbox("enabled", ref Globals.Config.Enabled);
                ImGui.TableSetColumnIndex(1);
                ImGui.InputInt("reserve (m)", ref Globals.Config.Reserve, 100);
                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Checkbox("auto att. wt.", ref Globals.Config.SkillBasedAttributeWeights);
                ImGui.TableSetColumnIndex(1);
                ImGui.InputInt("max res. (m)", ref Globals.Config.ReserveMax, 100);
                ImGui.EndTable();

                BuildCollapsingHeader("Attributes", PlayerConfiguration.traitIndex.strength, PlayerConfiguration.traitIndex.self);
                BuildCollapsingHeader("Vitals", PlayerConfiguration.traitIndex.health, PlayerConfiguration.traitIndex.mana);
                BuildCollapsingHeader("Skills", PlayerConfiguration.traitIndex.alchemy, PlayerConfiguration.traitIndex.weapontinkering);
            }
            catch (Exception ex)
            {
                PluginCore.Log(ex);
            }
        }

        private void BuildCollapsingHeader(string label, PlayerConfiguration.traitIndex start, PlayerConfiguration.traitIndex end)
        {
            if (ImGui.CollapsingHeader(label))
            {
                ImGui.BeginTable($"##{label}", 4);
                ImGui.TableHeadersRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text("Name");
                ImGui.TableSetColumnIndex(1);
                ImGui.Text($"Config Wt.");
                ImGui.TableSetColumnIndex(2);
                ImGui.Text($"Eff. Wt.");
                ImGui.TableSetColumnIndex(3);
                ImGui.Text($"Total XP");

                for (var i = start; i <= end; i++)
                    BuildTraitRow(i);
                ImGui.EndTable();
            }
        }

        void BuildTraitRow(PlayerConfiguration.traitIndex trait)
        {
            var label = trait.ToString();

            ImGui.TableNextRow();
            ImGui.TableSetColumnIndex(0);
            ImGui.Text(label);
            ImGui.TableSetColumnIndex(1);
            ImGui.PushItemWidth(-1);
            ImGui.InputInt($"##{label}", ref Globals.Config.Weights[(int)trait]);
            ImGui.PopItemWidth();
            ImGui.TableSetColumnIndex(2);
            ImGui.Text($"{(int)(Globals.XpAllocator._traitManager.Traits[trait.ToString()].EffectiveWeight+.5)}");
            ImGui.TableSetColumnIndex(3);
            var currentXp = Globals.XpAllocator._traitManager.Traits[trait.ToString()].CurrentXp;
            var totalXp = Globals.Core.CharacterFilter.TotalXP;
            ImGui.Text($"{CuteNumbers(currentXp)} ({currentXp*100.0/totalXp:0.#}%%)");
        }

        private string CuteNumbers(long currentXp)
        {
            // I should maybe cache these?
            var loops = 0;
            while (currentXp > 1000) {
                loops++;
                currentXp = (currentXp + 5) / 10;
            }
            double num = currentXp;
            while (loops % 3 != 0)
            {
                num = num / 10;
                loops++;
            }
            var suffix = loops switch
            {
                0 => "",
                3 => "k",
                6 => "m",
                9 => "b",
                _ => "oops"
            };
            return $"{num:0.#}{suffix}";
        }

        public void Dispose()
        {
            hud.Dispose();
        }
    }
}
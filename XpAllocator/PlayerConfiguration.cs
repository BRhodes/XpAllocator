using System;
using System.Collections.Generic;
using System.Linq;

namespace XpAllocator
{
    internal class PlayerConfiguration
    {
        public bool Enabled { get; set; }

        public long Reserve { get; set; }

        public double ReservePercent { get; set; }

        public bool SkillBasedAttributeWeights { get; set; }

        public long ReserveMax { get; set; }

        public Dictionary<string, double> Weights { get; set; }

        public double Weight(string key)
        {
            key = key.ToLower();
            if (Weights.ContainsKey(key))
                return Weights[key];
            return 0;
        }

        static public PlayerConfiguration Defaults()
        {
            var rv = new PlayerConfiguration();

            rv.Enabled = true;
            rv.Reserve = 0;
            rv.ReserveMax = 4000000000;
            rv.ReservePercent = .2;
            rv.SkillBasedAttributeWeights = true;

            rv.Weights = new Dictionary<string, double>();

            // attributes
            rv.Weights["strength"] = .2;
            rv.Weights["endurance"] = 0;
            rv.Weights["coordination"] = 0;
            rv.Weights["quickness"] = 0;
            rv.Weights["focus"] = 0;
            rv.Weights["self"] = 0;

            // vitals
            rv.Weights["health"] = 0.3;
            rv.Weights["stamina"] = 0.1;
            rv.Weights["mana"] = 0.1;

            // primary attacks
            rv.Weights["voidmagic"] = 1;
            rv.Weights["heavyweapons"] = 1;
            rv.Weights["lightweapons"] = 1;
            rv.Weights["finesseweapons"] = 1;
            rv.Weights["missileweapons"] = 1;
            rv.Weights["warmagic"] = 1;
            rv.Weights["twohandedcombat"] = 1;
            rv.Weights["summoning"] = 1;

            // defenses
            rv.Weights["meleedefense"] = 0.5;
            rv.Weights["missiledefense"] = 0.3;
            rv.Weights["magicdefense"] = 0.3;

            // magics
            rv.Weights["creatureenchantment"] = 0.3;
            rv.Weights["itemenchantment"] = 0.3;
            rv.Weights["lifemagic"] = 0.4;
            rv.Weights["manaconversion"] = 0.3;

            // movement
            rv.Weights["run"] = 0.01;
            rv.Weights["jump"] = 0.01;

            // lore
            rv.Weights["arcanelore"] = 0.05;

            // misc combat
            rv.Weights["healing"] = 0.2;
            rv.Weights["shield"] = 0.25;
            rv.Weights["dualwield"] = 0.01;
            rv.Weights["recklessness"] = 0.01;
            rv.Weights["sneakattack"] = 0.01;
            rv.Weights["dirtyfighting"] = 0.01;

            // misc useless
            rv.Weights["leadership"] = 0.01;
            rv.Weights["loyalty"] = 0.01;

            // crafting
            rv.Weights["skillsalvaging"] = 0.10;
            rv.Weights["itemtinkering"] = 0.10;
            rv.Weights["weapontinkering"] = 0.10;
            rv.Weights["armortinkering"] = 0.10;
            rv.Weights["magicitemtinkering"] = 0.10;
            rv.Weights["fletching"] = 0.15;
            rv.Weights["alchemy"] = 0.05;
            rv.Weights["cooking"] = 0.05;
            rv.Weights["lockpick"] = 0.05;

            rv.Weights["assessperson"] = 0.01;
            rv.Weights["deception"] = 0.01;
            rv.Weights["assesscreature"] = 0.01;

            return rv;
        }


        internal void SetWeight(string trait, double weight)
        {
            if (GameConstants.SkillData.Any(x => x.Name == trait) || GameConstants.NonSkillTraits.Any(x => x == trait))
            {
                Weights[trait] = weight;
            }
            else
            {
                Util.WriteToChat($"Unable to set weight for unknown trait: {trait}");
            }
        }
    }
}
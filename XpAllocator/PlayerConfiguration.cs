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

        public Dictionary<string, int> Weights { get; set; }

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

            rv.Weights = new Dictionary<string, int>();

            // attributes
            rv.Weights["strength"] = 20;
            rv.Weights["endurance"] = 0;
            rv.Weights["coordination"] = 0;
            rv.Weights["quickness"] = 0;
            rv.Weights["focus"] = 0;
            rv.Weights["self"] = 0;

            // vitals
            rv.Weights["health"] = 030;
            rv.Weights["stamina"] = 010;
            rv.Weights["mana"] = 010;

            // primary attacks
            rv.Weights["voidmagic"] = 100;
            rv.Weights["heavyweapons"] = 100;
            rv.Weights["lightweapons"] = 100;
            rv.Weights["finesseweapons"] = 100;
            rv.Weights["missileweapons"] = 100;
            rv.Weights["warmagic"] = 100;
            rv.Weights["twohandedcombat"] = 100;
            rv.Weights["summoning"] = 100;

            // defenses
            rv.Weights["meleedefense"] = 50;
            rv.Weights["missiledefense"] = 30;
            rv.Weights["magicdefense"] = 30;

            // magics
            rv.Weights["creatureenchantment"] = 30;
            rv.Weights["itemenchantment"] = 30;
            rv.Weights["lifemagic"] = 40;
            rv.Weights["manaconversion"] = 30;

            // movement
            rv.Weights["run"] = 10;
            rv.Weights["jump"] = 10;

            // lore
            rv.Weights["arcanelore"] = 5;

            // misc combat
            rv.Weights["healing"] = 20;
            rv.Weights["shield"] = 25;
            rv.Weights["dualwield"] = 10;
            rv.Weights["recklessness"] = 10;
            rv.Weights["sneakattack"] = 10;
            rv.Weights["dirtyfighting"] = 10;

            // misc useless
            rv.Weights["leadership"] = 1;
            rv.Weights["loyalty"] = 1;

            // crafting
            rv.Weights["salvaging"] = 10;
            rv.Weights["itemtinkering"] = 10;
            rv.Weights["weapontinkering"] = 10;
            rv.Weights["armortinkering"] = 10;
            rv.Weights["magicitemtinkering"] = 10;
            rv.Weights["fletching"] = 15;
            rv.Weights["alchemy"] = 5;
            rv.Weights["cooking"] = 5;
            rv.Weights["lockpick"] = 5;

            rv.Weights["assessperson"] = 1;
            rv.Weights["deception"] = 1;
            rv.Weights["assesscreature"] = 1;

            return rv;
        }


        internal void SetWeight(string trait, int weight)
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
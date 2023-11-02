using Newtonsoft.Json;
using System.Numerics;

namespace XpAllocator
{
    internal class PlayerConfiguration
    {
        public enum traitIndex
        {
            strength,
            endurance,
            coordination,
            quickness,
            focus,
            self,
            health,
            stamina,
            mana,
            alchemy,
            arcanelore,
            armortinkering,
            assesscreature,
            assessperson,
            cooking,
            creatureenchantment,
            deception,
            dirtyfighting,
            dualwield,
            finesseweapons,
            fletching,
            healing,
            heavyweapons,
            itemenchantment,
            itemtinkering,
            jump,
            leadership,
            lifemagic,
            lightweapons,
            lockpick,
            loyalty,
            magicdefense,
            magicitemtinkering,
            manaconversion,
            meleedefense,
            missiledefense,
            missileweapons,
            recklessness,
            run,
            salvaging,
            shield,
            sneakattack,
            summoning,
            twohandedcombat,
            voidmagic,
            warmagic,
            weapontinkering,
        }

        public bool Enabled;
        public bool SkillBasedAttributeWeights;
        public int ReservePercent;
        public int Reserve;
        public int ReserveMax;
        public int[] Weights { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Pos { get; set; }

        [JsonIgnore]
        public bool PositionSet { get; set; } = false;

        static public PlayerConfiguration Defaults()
        {
            var rv = new PlayerConfiguration();

            rv.Enabled = false;
            rv.Reserve = 0;
            rv.ReserveMax = 4000;
            rv.ReservePercent = 20;
            rv.SkillBasedAttributeWeights = true;
            rv.Size = new Vector2(400, 400);
            rv.Pos = new Vector2(200, 120);

            rv.Weights = new int[(int)traitIndex.weapontinkering+1];

            // attributes
            rv.Weights[(int)traitIndex.strength] = 20;
            rv.Weights[(int)traitIndex.endurance] = 0;
            rv.Weights[(int)traitIndex.coordination] = 0;
            rv.Weights[(int)traitIndex.quickness] = 0;
            rv.Weights[(int)traitIndex.focus] = 0;
            rv.Weights[(int)traitIndex.self] = 0;

            // vitals
            rv.Weights[(int)traitIndex.health] = 030;
            rv.Weights[(int)traitIndex.stamina] = 010;
            rv.Weights[(int)traitIndex.mana] = 010;

            // primary attacks
            rv.Weights[(int)traitIndex.voidmagic] = 100;
            rv.Weights[(int)traitIndex.lightweapons] = 100;
            rv.Weights[(int)traitIndex.finesseweapons] = 100;
            rv.Weights[(int)traitIndex.heavyweapons] = 100;
            rv.Weights[(int)traitIndex.missileweapons] = 100;
            rv.Weights[(int)traitIndex.warmagic] = 100;
            rv.Weights[(int)traitIndex.twohandedcombat] = 100;
            rv.Weights[(int)traitIndex.summoning] = 100;

            // defenses
            rv.Weights[(int)traitIndex.meleedefense] = 50;
            rv.Weights[(int)traitIndex.missiledefense] = 30;
            rv.Weights[(int)traitIndex.magicdefense] = 30;

            // magics
            rv.Weights[(int)traitIndex.creatureenchantment] = 30;
            rv.Weights[(int)traitIndex.itemenchantment] = 30;
            rv.Weights[(int)traitIndex.lifemagic] = 40;
            rv.Weights[(int)traitIndex.manaconversion] = 30;

            // movement
            rv.Weights[(int)traitIndex.run] = 10;
            rv.Weights[(int)traitIndex.jump] = 10;

            // lore
            rv.Weights[(int)traitIndex.arcanelore] = 5;

            // misc combat
            rv.Weights[(int)traitIndex.healing] = 20;
            rv.Weights[(int)traitIndex.shield] = 25;
            rv.Weights[(int)traitIndex.dualwield] = 10;
            rv.Weights[(int)traitIndex.recklessness] = 10;
            rv.Weights[(int)traitIndex.sneakattack] = 10;
            rv.Weights[(int)traitIndex.dirtyfighting] = 10;

            // misc useless
            rv.Weights[(int)traitIndex.leadership] = 1;
            rv.Weights[(int)traitIndex.loyalty] = 1;

            // crafting
            rv.Weights[(int)traitIndex.salvaging] = 10;
            rv.Weights[(int)traitIndex.itemtinkering] = 10;
            rv.Weights[(int)traitIndex.weapontinkering] = 10;
            rv.Weights[(int)traitIndex.armortinkering] = 10;
            rv.Weights[(int)traitIndex.magicitemtinkering] = 10;
            rv.Weights[(int)traitIndex.fletching] = 15;
            rv.Weights[(int)traitIndex.alchemy] = 5;
            rv.Weights[(int)traitIndex.cooking] = 5;
            rv.Weights[(int)traitIndex.lockpick] = 5;

            rv.Weights[(int)traitIndex.assessperson] = 1;
            rv.Weights[(int)traitIndex.deception] = 1;
            rv.Weights[(int)traitIndex.assesscreature] = 1;

            return rv;
        }
    }
}
//using Decal.Adapter.Wrappers;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace XpAllocator
//{
//    static public class DefaultConfig
//    {
//        public static Dictionary<AttributeType, double> AttributeDefaultWeights()
//        {
//            Dictionary<AttributeType, double> rv = new Dictionary<AttributeType, double>();

//            rv[AttributeType.CurrentStrength] = 0.25;
//            rv[AttributeType.CurrentEndurance] = 0.10;
//            rv[AttributeType.CurrentCoordination] = 0.10;
//            rv[AttributeType.CurrentQuickness] = 0.10;
//            rv[AttributeType.CurrentFocus] = 0.10;
//            rv[AttributeType.CurrentSelf] = 0.10;

//            IList<KeyValuePair<SkillType, KeyValuePair<AttributeType, int>>> skillFactors = GameConstants.SkillSynergies;
//            Dictionary<SkillType, double> skillWeights = SkillDefaultWeights();


//            foreach (KeyValuePair<SkillType, KeyValuePair<AttributeType, int>> kvp in skillFactors)
//            {
//                SkillType skill = kvp.Key;
//                AttributeType att = kvp.Value.Key;
//                int factor = kvp.Value.Value;
//                if (Globals.Core.Actions.SkillTrainLevel[skill] >= 2)
//                {
//                    rv[att] += skillWeights[skill] / factor;
//                }
//            }

//            return rv;
//        }
//        public static Dictionary<VitalType, double> VitalDefaultWeights()
//        {
//            Dictionary<VitalType, double> rv = new Dictionary<VitalType, double>();

//            rv[VitalType.MaximumHealth] = 0.3;
//            rv[VitalType.MaximumStamina] = 0.1;
//            rv[VitalType.MaximumMana] = 0.1;

//            return rv;
//        }
//        public static Dictionary<SkillType, double> TrainSkillDefaultWeights()
//        {
//            Dictionary<SkillType, double> rv = new Dictionary<SkillType, double>();
//            Dictionary<SkillType, double> all = SkillDefaultWeights();

//            foreach (KeyValuePair<SkillType, double> kvp in all)
//            {
//                if (Globals.Core.Actions.SkillTrainLevel[kvp.Key] == 2)
//                    rv[kvp.Key] = kvp.Value;
//            }

//            return rv;
//        }

//        public static Dictionary<SkillType, double> SpecSkillDefaultWeights()
//        {
//            Dictionary<SkillType, double> rv = new Dictionary<SkillType, double>();
//            Dictionary<SkillType, double> all = SkillDefaultWeights();

//            foreach (KeyValuePair<SkillType, double> kvp in all)
//            {
//                if (Globals.Core.Actions.SkillTrainLevel[kvp.Key] == 3)
//                    rv[kvp.Key] = kvp.Value;
//            }

//            return rv;
//        }

//        public static Dictionary<SkillType, double> SkillDefaultWeights()
//        {
//            Dictionary<SkillType, double> rv = new Dictionary<SkillType, double>
//            {
//                // Primary Attacks
//                [SkillType.CurrentVoidMagic] = 1,
//                [SkillType.CurrentHeavyWeapons] = 1,
//                [SkillType.CurrentLightWeapons] = 1,
//                [SkillType.CurrentFinesseWeapons] = 1,
//                [SkillType.CurrentMissileWeapons] = 1,
//                [SkillType.CurrentWarMagic] = 1,
//                [SkillType.CurrentTwoHandedCombat] = 1,
//                [SkillType.CurrentSummoning] = 1,

//                // Defenses
//                [SkillType.CurrentMeleeDefense] = 0.5,
//                [SkillType.CurrentMissileDefense] = 0.3,
//                [SkillType.CurrentMagicDefense] = 0.3,

//                // Magics
//                [SkillType.CurrentCreatureEnchantment] = 0.3,
//                [SkillType.CurrentItemEnchantment] = 0.3,
//                [SkillType.CurrentLifeMagic] = 0.4,
//                [SkillType.CurrentManaConversion] = 0.3,

//                // Misc Movement
//                [SkillType.CurrentRun] = 0.01,
//                [SkillType.CurrentJump] = 0.01,

//                // Misc Useful
//                [SkillType.CurrentArcaneLore] = 0.05,

//                // Misc Combat
//                //[SkillType.CurrentHealing] = 0.2,
//                //[SkillType.CurrentShield] = 0.25,
//                //[SkillType.CurrentDualWield] = 0.40,
//                //[SkillType.CurrentRecklessness] = 0.2,
//                //[SkillType.CurrentSneakAttack] = 0.2,
//                //[SkillType.CurrentDirtyFighting] = 0.2,
//                [SkillType.CurrentHealing] = 0.2,
//                [SkillType.CurrentShield] = 0.25,
//                [SkillType.CurrentDualWield] = 0.01,
//                [SkillType.CurrentRecklessness] = 0.01,
//                [SkillType.CurrentSneakAttack] = 0.01,
//                [SkillType.CurrentDirtyFighting] = 0.01,

//                // Misc Useless
//                [SkillType.CurrentLeadership] = 0.01,
//                [SkillType.CurrentLoyalty] = 0.01,

//                // Crafting
//                [SkillType.CurrentSkillSalvaging] = 0.10,
//                [SkillType.CurrentItemTinkering] = 0.10,
//                [SkillType.CurrentWeaponTinkering] = 0.10,
//                [SkillType.CurrentArmorTinkering] = 0.10,
//                [SkillType.CurrentMagicItemTinkering] = 0.10,
//                [SkillType.CurrentGearcraft] = 0.05,
//                [SkillType.CurrentFletchingSkill] = 0.01,
//                //[SkillType.CurrentFletchingSkill] = 0.15,
//                [SkillType.CurrentAlchemySkill] = 0.05,
//                [SkillType.CurrentCookingSkill] = 0.05,
//                [SkillType.CurrentLockpick] = 0.05,

//                [SkillType.CurrentAssessPerson] = 0.01,
//                [SkillType.CurrentDeception] = 0.01,
//                [SkillType.CurrentAssessCreature] = 0.01
//            };

//            return rv;
//        }
//    }
//}

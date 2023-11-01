using Decal.Adapter.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace XpAllocator
{
    class TraitManager
    {
        private readonly Dictionary<string, ITrait> traits;

        public TraitManager(PlayerConfiguration config)
        {
            traits = InitializeTraits(config);
        }

        public long ExpectedRaiseCost()
        {
            if (traits.Count == 0) return long.MaxValue;
            var orderedTraits = traits.OrderBy(x => x.Value.AllocationWeight());
            var traitToRaise = orderedTraits.First().Value;

            //var pruneCandidate = orderedTraits.Last();
            //if (!pruneCandidate.Value.CanBeRaised()) traits.Remove(pruneCandidate.Key);

            return traitToRaise.RaiseCost();
        }

        public RaiseAttempt RaiseTrait()
        {
            if (traits.Count == 0) return null;
            var orderedTraits = traits.OrderBy(x => x.Value.AllocationWeight());
            var traitToRaise = orderedTraits.First().Value;

            var raiseCost = traitToRaise.RaiseCost();

            if (raiseCost > Globals.Core.CharacterFilter.UnassignedXP)
                return null;
            
            return traitToRaise.Raise();
        }

        public Dictionary<string, ITrait> InitializeTraits(PlayerConfiguration _config)
        {
            var rv = new Dictionary<string, ITrait>();
            //Attributes
            rv["strength"] = new Attribute(AttributeType.CurrentStrength, _config.Weight("strength"));
            rv["endurance"] = new Attribute(AttributeType.CurrentEndurance, _config.Weight("endurance"));
            rv["coordination"] = new Attribute(AttributeType.CurrentCoordination, _config.Weight("coordination"));
            rv["quickness"] = new Attribute(AttributeType.CurrentQuickness, _config.Weight("quickness"));
            rv["focus"] = new Attribute(AttributeType.CurrentFocus, _config.Weight("focus"));
            rv["self"] = new Attribute(AttributeType.CurrentSelf, _config.Weight("self"));

            //Vitals
            rv["health"] = new Vital(VitalType.MaximumHealth, _config.Weight("health"));
            rv["stamina"] = new Vital(VitalType.MaximumStamina, _config.Weight("stamina"));
            rv["mana"] = new Vital(VitalType.MaximumMana, _config.Weight("mana"));

            //Skills
            rv["alchemy"] = new Skill(SkillType.CurrentAlchemySkill, _config.Weight("alchemy"));
            rv["arcanelore"] = new Skill(SkillType.CurrentArcaneLore, _config.Weight("arcanelore"));
            rv["armortinkering"] = new Skill(SkillType.CurrentArmorTinkering, _config.Weight("armortinkering"));
            rv["assesscreature"] = new Skill(SkillType.CurrentAssessCreature, _config.Weight("assesscreature"));
            rv["assessperson"] = new Skill(SkillType.CurrentAssessPerson, _config.Weight("assessperson"));
            rv["cooking"] = new Skill(SkillType.CurrentCookingSkill, _config.Weight("cooking"));
            rv["creatureenchantment"] = new Skill(SkillType.CurrentCreatureEnchantment, _config.Weight("creatureenchantment"));
            rv["deception"] = new Skill(SkillType.CurrentDeception, _config.Weight("deception"));
            rv["dirtyfighting"] = new Skill(SkillType.CurrentDirtyFighting, _config.Weight("dirtyfighting"));
            rv["dualwield"] = new Skill(SkillType.CurrentDualWield, _config.Weight("dualwield"));
            rv["finesseweapons"] = new Skill(SkillType.CurrentFinesseWeapons, _config.Weight("finesseweapons"));
            rv["fletching"] = new Skill(SkillType.CurrentFletchingSkill, _config.Weight("fletching"));
            rv["healing"] = new Skill(SkillType.CurrentHealing, _config.Weight("healing"));
            rv["heavyweapons"] = new Skill(SkillType.CurrentHeavyWeapons, _config.Weight("heavyweapons"));
            rv["itemenchantment"] = new Skill(SkillType.CurrentItemEnchantment, _config.Weight("itemenchantment"));
            rv["itemtinkering"] = new Skill(SkillType.CurrentItemTinkering, _config.Weight("itemtinkering"));
            rv["jump"] = new Skill(SkillType.CurrentJump, _config.Weight("jump"));
            rv["leadership"] = new Skill(SkillType.CurrentLeadership, _config.Weight("leadership"));
            rv["lifemagic"] = new Skill(SkillType.CurrentLifeMagic, _config.Weight("lifemagic"));
            rv["lightweapons"] = new Skill(SkillType.CurrentLightWeapons, _config.Weight("lightweapons"));
            rv["lockpick"] = new Skill(SkillType.CurrentLockpick, _config.Weight("lockpick"));
            rv["loyalty"] = new Skill(SkillType.CurrentLoyalty, _config.Weight("loyalty"));
            rv["magicdefense"] = new Skill(SkillType.CurrentMagicDefense, _config.Weight("magicdefense"));
            rv["magicitemtinkering"] = new Skill(SkillType.CurrentMagicItemTinkering, _config.Weight("magicitemtinkering"));
            rv["manaconversion"] = new Skill(SkillType.CurrentManaConversion, _config.Weight("manaconversion"));
            rv["meleedefense"] = new Skill(SkillType.CurrentMeleeDefense, _config.Weight("meleedefense"));
            rv["missiledefense"] = new Skill(SkillType.CurrentMissileDefense, _config.Weight("missiledefense"));
            rv["missileweapons"] = new Skill(SkillType.CurrentMissileWeapons, _config.Weight("missileweapons"));
            rv["recklessness"] = new Skill(SkillType.CurrentRecklessness, _config.Weight("recklessness"));
            rv["run"] = new Skill(SkillType.CurrentRun, _config.Weight("run"));
            rv["salvaging"] = new Skill(SkillType.CurrentSkillSalvaging, _config.Weight("salvaging"));
            rv["shield"] = new Skill(SkillType.CurrentShield, _config.Weight("shield"));
            rv["sneakattack"] = new Skill(SkillType.CurrentSneakAttack, _config.Weight("sneakattack"));
            rv["summoning"] = new Skill(SkillType.CurrentSummoning, _config.Weight("summoning"));
            rv["twohandedcombat"] = new Skill(SkillType.CurrentTwoHandedCombat, _config.Weight("twohandedcombat"));
            rv["voidmagic"] = new Skill(SkillType.CurrentVoidMagic, _config.Weight("voidmagic"));
            rv["warmagic"] = new Skill(SkillType.CurrentWarMagic, _config.Weight("warmagic"));
            rv["weapontinkering"] = new Skill(SkillType.CurrentWeaponTinkering, _config.Weight("weapontinkering"));

            if (_config.SkillBasedAttributeWeights)
            {
                foreach (var synergy in GameConstants.SkillData)
                {
                    foreach (var attribute in synergy.SynergyAttributes)
                    {
                        var syn = rv[synergy.Name] as Skill;
                        if (syn != null && syn.TrainLevel >= 2)
                        {
                            rv[attribute].Weight += rv[synergy.Name].Weight / synergy.SynergyRatio;
                        }
                    }
                }

                rv["endurance"].Weight += rv["health"].Weight / 2;
                rv["endurance"].Weight += rv["stamina"].Weight;
                rv["self"].Weight += rv["mana"].Weight;
            }

            var traitKvps = rv.ToList();
            foreach (var traitKvp in traitKvps)
            {
                if (!traitKvp.Value.CanBeRaised())
                {
                    rv.Remove(traitKvp.Key);
                }
            }

            return rv;
        }

        internal string DumpWeights()
        {
            return string.Join("\n", traits.Select(x => $"{x.Key}: {x.Value.Weight}"));
        }
    }
}

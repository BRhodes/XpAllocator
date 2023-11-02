using Decal.Adapter.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace XpAllocator
{
    class TraitManager
    {
        public readonly Dictionary<string, ITrait> Traits;

        public TraitManager(PlayerConfiguration config)
        {
            Traits = InitializeTraits(config);
        }

        public long ExpectedRaiseCost()
        {
            if (Traits.Count == 0) return long.MaxValue;
            var orderedTraits = Traits.OrderBy(x => x.Value.AllocationWeight());
            var traitToRaise = orderedTraits.First().Value;

            return traitToRaise.RaiseCost();
        }

        public RaiseAttempt RaiseTrait()
        {
            if (Traits.Count == 0) return null;
            var orderedTraits = Traits.OrderBy(x => x.Value.AllocationWeight());
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
            rv["strength"] = new Attribute("strength", AttributeType.CurrentStrength);
            rv["endurance"] = new Attribute("endurance", AttributeType.CurrentEndurance);
            rv["coordination"] = new Attribute("coordination", AttributeType.CurrentCoordination);
            rv["quickness"] = new Attribute("quickness", AttributeType.CurrentQuickness);
            rv["focus"] = new Attribute("focus", AttributeType.CurrentFocus);
            rv["self"] = new Attribute("self", AttributeType.CurrentSelf);

            //Vitals
            rv["health"] = new Vital("health", VitalType.MaximumHealth);
            rv["stamina"] = new Vital("stamina", VitalType.MaximumStamina);
            rv["mana"] = new Vital("mana", VitalType.MaximumMana);

            //Skills
            rv["alchemy"] = new Skill("alchemy", SkillType.CurrentAlchemySkill);
            rv["arcanelore"] = new Skill("arcanelore", SkillType.CurrentArcaneLore);
            rv["armortinkering"] = new Skill("armortinkering", SkillType.CurrentArmorTinkering);
            rv["assesscreature"] = new Skill("assesscreature", SkillType.CurrentAssessCreature);
            rv["assessperson"] = new Skill("assessperson", SkillType.CurrentAssessPerson);
            rv["cooking"] = new Skill("cooking", SkillType.CurrentCookingSkill);
            rv["creatureenchantment"] = new Skill("creatureenchantment", SkillType.CurrentCreatureEnchantment);
            rv["deception"] = new Skill("deception", SkillType.CurrentDeception);
            rv["dirtyfighting"] = new Skill("dirtyfighting", SkillType.CurrentDirtyFighting);
            rv["dualwield"] = new Skill("dualwield", SkillType.CurrentDualWield);
            rv["finesseweapons"] = new Skill("finesseweapons", SkillType.CurrentFinesseWeapons);
            rv["fletching"] = new Skill("fletching", SkillType.CurrentFletchingSkill);
            rv["healing"] = new Skill("healing", SkillType.CurrentHealing);
            rv["heavyweapons"] = new Skill("heavyweapons", SkillType.CurrentHeavyWeapons);
            rv["itemenchantment"] = new Skill("itemenchantment", SkillType.CurrentItemEnchantment);
            rv["itemtinkering"] = new Skill("itemtinkering", SkillType.CurrentItemTinkering);
            rv["jump"] = new Skill("jump", SkillType.CurrentJump);
            rv["leadership"] = new Skill("leadership", SkillType.CurrentLeadership);
            rv["lifemagic"] = new Skill("lifemagic", SkillType.CurrentLifeMagic);
            rv["lightweapons"] = new Skill("lightweapons", SkillType.CurrentLightWeapons);
            rv["lockpick"] = new Skill("lockpick", SkillType.CurrentLockpick);
            rv["loyalty"] = new Skill("loyalty", SkillType.CurrentLoyalty);
            rv["magicdefense"] = new Skill("magicdefense", SkillType.CurrentMagicDefense);
            rv["magicitemtinkering"] = new Skill("magicitemtinkering", SkillType.CurrentMagicItemTinkering);
            rv["manaconversion"] = new Skill("manaconversion", SkillType.CurrentManaConversion);
            rv["meleedefense"] = new Skill("meleedefense", SkillType.CurrentMeleeDefense);
            rv["missiledefense"] = new Skill("missiledefense", SkillType.CurrentMissileDefense);
            rv["missileweapons"] = new Skill("missileweapons", SkillType.CurrentMissileWeapons);
            rv["recklessness"] = new Skill("recklessness", SkillType.CurrentRecklessness);
            rv["run"] = new Skill("run", SkillType.CurrentRun);
            rv["salvaging"] = new Skill("salvaging", SkillType.CurrentSkillSalvaging);
            rv["shield"] = new Skill("shield", SkillType.CurrentShield);
            rv["sneakattack"] = new Skill("sneakattack", SkillType.CurrentSneakAttack);
            rv["summoning"] = new Skill("summoning", SkillType.CurrentSummoning);
            rv["twohandedcombat"] = new Skill("twohandedcombat", SkillType.CurrentTwoHandedCombat);
            rv["voidmagic"] = new Skill("voidmagic", SkillType.CurrentVoidMagic);
            rv["warmagic"] = new Skill("warmagic", SkillType.CurrentWarMagic);
            rv["weapontinkering"] = new Skill("weapontinkering", SkillType.CurrentWeaponTinkering);


            foreach (var synergy in GameConstants.SkillData)
            {
                foreach (var attribute in synergy.SynergyAttributes)
                {
                    var attTrait = rv[attribute] as Attribute;
                    attTrait.Synergies.Add((rv[synergy.Name], synergy.SynergyRatio));
                }
            }

            var end = rv["endurance"] as Attribute;
            end.Synergies.Add((rv["health"], 2));
            end.Synergies.Add((rv["stamina"], 2));

            var self = rv["self"] as Attribute;
            self.Synergies.Add((rv["mana"], 2));
            
            return rv;
        }
    }
}

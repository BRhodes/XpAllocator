using Decal.Adapter.Wrappers;
using System;
using System.Collections.Generic;
using System.Drawing.Text;

namespace XpAllocator
{
    internal class Attribute : Trait<AttributeType>
    {
        public Attribute(AttributeType attribute, double weight) : base(attribute, weight)
        {
            XpTable = GameConstants.AttributeXpTable;
            LevelHook = Globals.Core.Actions.AttributeClicks;
            TotalXpHook = Globals.Core.Actions.AttributeTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddAttributeExperience(attribute, x);
        }
    }

    internal class Vital : Trait<VitalType>
    {
        public Vital(VitalType vital, double weight) : base(vital, weight)
        {
            XpTable = GameConstants.VitalXpTable;
            LevelHook = Globals.Core.Actions.VitalClicks;
            TotalXpHook = Globals.Core.Actions.VitalTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddVitalExperience(vital, x);
        }
    }
    internal class Skill : Trait<SkillType>
    {
        public int TrainLevel { get; private set; }

        public Skill(SkillType skill, double weight) : base(skill, weight)
        {
            LevelHook = Globals.Core.Actions.SkillClicks;
            TotalXpHook = Globals.Core.Actions.SkillTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddSkillExperience(skill, x);

            TrainLevel = Globals.Core.Actions.SkillTrainLevel[skill];
            switch (TrainLevel)
            {
                case 3: // specialized
                    XpTable = GameConstants.SpecSkillXpTable;
                    break;
                case 2: // trained
                    XpTable = GameConstants.TrainedSkillXpTable;
                    break;
                default:
                    XpTable = null;
                    break;
            }
        }
    }

    internal class Trait<T> : ITrait where T: struct, IConvertible
    {
        readonly T _trait;
        private int MaxLevel => XpTable.Count - 1;
        protected IList<long> XpTable;

        protected HookIndexer<T> LevelHook;
        virtual protected int CurrentLevel => LevelHook[_trait];

        protected HookIndexer<T> TotalXpHook;
        int CurrentXp => TotalXpHook[_trait];
        public Action<int> RaiseTraitDelegate { get; set; }

        public double Weight { get; set; }

        public virtual double EffectiveWeight => Weight;

        public double AllocationWeight()
        {
            return Weight == 0 ? double.MaxValue : RaiseCost() / Weight;
        }

        public Trait(T trait, double weight)
        {
            _trait = trait;
            Weight = weight;
        }

        public long RaiseCost()
        {
            if (!CanBeRaised()) return long.MaxValue;
            long expAtNextLevel = XpTable[CurrentLevel + 1];
            var cost = (int)(expAtNextLevel - CurrentXp);

            return cost;
        }

        public RaiseAttempt Raise()
        {
            var raiseCost = RaiseCost();
            RaiseAttempt raiseAttempt = new RaiseAttempt();

            if (Globals.Core.CharacterFilter.UnassignedXP > raiseCost && raiseCost > 0)
            {
                raiseAttempt.Trait = _trait.ToString();
                raiseAttempt.XpAllocated = raiseCost;

                RaiseTraitDelegate((int)raiseCost);

                return raiseAttempt;
            }
            return null;
        }

        public bool CanBeRaised()
        {
            return XpTable != null && CurrentLevel < MaxLevel && Weight > 0;
        }
    }
}
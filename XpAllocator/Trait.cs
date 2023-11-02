using ACE.DatLoader.FileTypes;
using Decal.Adapter.Wrappers;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using UtilityBelt.Service.Lib.Settings;

namespace XpAllocator
{
    internal class Attribute : Trait<AttributeType>
    {
        protected override IList<long> XpTable => GameConstants.AttributeXpTable;
        public List<(ITrait, int)> Synergies = new();
        public override double EffectiveWeight => Weight + Synergies.Sum(x => x.Item1.EffectiveWeight / x.Item2);

        public Attribute(string name, AttributeType decalName) : base(name, decalName)
        {
            LevelHook = Globals.Core.Actions.AttributeClicks;
            TotalXpHook = Globals.Core.Actions.AttributeTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddAttributeExperience(decalName, x);
        }
    }

    internal class Vital : Trait<VitalType>
    {
        protected override IList<long> XpTable => GameConstants.VitalXpTable;

        public Vital(string name, VitalType decalName) : base(name, decalName)
        {
            LevelHook = Globals.Core.Actions.VitalClicks;
            TotalXpHook = Globals.Core.Actions.VitalTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddVitalExperience(decalName, x);
        }
    }

    internal class Skill : Trait<SkillType>
    {
        protected override IList<long> XpTable
        { 
            get
            {
                return TrainLevel switch
                {
                    // specialized
                    3 => GameConstants.SpecSkillXpTable,
                    // trained
                    2 => GameConstants.TrainedSkillXpTable,
                    _ => null,
                };
            }
        }

        public int TrainLevel => Globals.Core.Actions.SkillTrainLevel[_decalName];

        public Skill(string name, SkillType decalName) : base(name, decalName)
        {
            LevelHook = Globals.Core.Actions.SkillClicks;
            TotalXpHook = Globals.Core.Actions.SkillTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddSkillExperience(decalName, x);
        }
    }

    abstract internal class Trait<TraitEnum> : ITrait where TraitEnum: struct, IConvertible
    {
        protected readonly TraitEnum _decalName;
        readonly string _name;
        private int MaxLevel => XpTable.Count - 1;

        abstract protected IList<long> XpTable { get; }

        protected HookIndexer<TraitEnum> LevelHook;
        virtual protected int CurrentLevel => LevelHook[_decalName];

        protected HookIndexer<TraitEnum> TotalXpHook;
        int CurrentXp => TotalXpHook[_decalName];
        public Action<int> RaiseTraitDelegate { get; set; }

        public int Weight => Globals.Config.Weights[_name];

        public virtual double EffectiveWeight => CanBeRaised() ? Weight : 0;

        public double AllocationWeight()
        {
            return Weight == 0 ? double.MaxValue : RaiseCost() / (double) Weight;
        }

        public Trait(string name, TraitEnum decalName)
        {
            _decalName = decalName;
            _name = name;
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
            RaiseAttempt raiseAttempt = new();

            if (Globals.Core.CharacterFilter.UnassignedXP > raiseCost && raiseCost > 0)
            {
                raiseAttempt.Trait = _decalName.ToString();
                raiseAttempt.XpAllocated = raiseCost;

                RaiseTraitDelegate((int)raiseCost);

                return raiseAttempt;
            }
            return null;
        }

        public bool CanBeRaised()
        {
            return XpTable != null && CurrentLevel < MaxLevel;
        }
    }
}
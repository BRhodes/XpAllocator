using Decal.Adapter.Wrappers;
using System;
using System.Collections.Generic;
using static XpAllocator.PlayerConfiguration;

namespace XpAllocator
{
    abstract internal class Trait<TraitEnum> : ITrait where TraitEnum: struct, IConvertible
    {
        protected readonly TraitEnum _decalName;
        readonly string _name;
        private int MaxLevel => XpTable.Count - 1;

        abstract protected IList<long> XpTable { get; }

        protected HookIndexer<TraitEnum> LevelHook;
        virtual protected int CurrentLevel => LevelHook[_decalName];

        protected HookIndexer<TraitEnum> TotalXpHook;
        public long CurrentXp => (uint)TotalXpHook[_decalName];
        public Action<int> RaiseTraitDelegate { get; set; }

        public int Weight => Globals.Config.Weights[(int)Enum.Parse(typeof(traitIndex), _name)];

        public virtual double EffectiveWeight => XpTable != null ? Weight : 0;

        public double AllocationWeight()
        {
            return EffectiveWeight <= 0 ? double.MaxValue : RaiseCost() / (double)EffectiveWeight;
        }

        public Trait(string name, TraitEnum decalName)
        {
            _decalName = decalName;
            _name = name;
        }

        public long RaiseCost()
        {
            if (!(XpTable != null && CurrentLevel < MaxLevel)) return long.MaxValue;
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
    }
}
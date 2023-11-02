using Decal.Adapter.Wrappers;
using System.Collections.Generic;
using System.Linq;

namespace XpAllocator
{
    internal class Attribute : Trait<AttributeType>
    {
        protected override IList<long> XpTable => GameConstants.AttributeXpTable;
        public List<(ITrait, int)> Synergies = new();
        public override double EffectiveWeight => Weight + (Globals.Config.SkillBasedAttributeWeights ? Synergies.Sum(x => x.Item1.EffectiveWeight / x.Item2) : 0);

        public Attribute(string name, AttributeType decalName) : base(name, decalName)
        {
            LevelHook = Globals.Core.Actions.AttributeClicks;
            TotalXpHook = Globals.Core.Actions.AttributeTotalXP;
            RaiseTraitDelegate = (x) => Globals.Core.Actions.AddAttributeExperience(decalName, x);
        }
    }
}
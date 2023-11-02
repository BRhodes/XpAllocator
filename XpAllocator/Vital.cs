using Decal.Adapter.Wrappers;
using System.Collections.Generic;

namespace XpAllocator
{
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
}
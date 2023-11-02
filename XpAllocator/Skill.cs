using Decal.Adapter.Wrappers;
using System.Collections.Generic;

namespace XpAllocator
{
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
}
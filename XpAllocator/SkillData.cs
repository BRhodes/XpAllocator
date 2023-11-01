using System.Collections.Generic;

namespace XpAllocator
{
    internal class SkillData
    {
        public string Name { get; private set; }
        public int SynergyRatio { get; private set; }
        public IList<string> SynergyAttributes { get; private set; }

        public SkillData(string name, int synergyRatio, List<string> synergyAttributes)
        {
            Name = name;
            SynergyRatio = synergyRatio;
            SynergyAttributes = synergyAttributes.AsReadOnly();
        }
    }
}
using System.Collections.Generic;
using com.enemyhideout.loot;

namespace Assets.Code.Editor
{
    /// <summary>
    /// A test case that can be used to test <see cref="LootCore.RollForItem"/>
    /// </summary>
    public class RollForItemTestCase
    {
        public string Description;
        public List<LootTableEntry> Entries;
        public float RandomValue;
        public string Expected;

        public override string ToString()
        {
            return Description;
        }
    }
}
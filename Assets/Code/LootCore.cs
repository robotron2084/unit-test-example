using System.Collections.Generic;
using System.Linq;

namespace com.enemyhideout.loot
{
    public static class LootCore
    {
        // Its good to create testable functions, with well-defined behavior and outcomes. And comment them!
        
        /// <summary>
        /// For the given entries, return a random item from them.
        /// </summary>
        /// <param name="tableEntries">The entries of the loot table we're interested in.</param>
        /// <param name="random">A source of randomness.</param>
        /// <returns>The item of an entry if found, or null.</returns>
        public static string RollForItem(List<LootTableEntry> tableEntries, IRandom random)
        {
            float maxValue = tableEntries.Sum(x => x.Weight);
            float randValue = random.Range(0.0f, maxValue);

            float minValue = 0.0f;
            foreach (var entry in tableEntries)
            {
                if (InRange(randValue, minValue, minValue + entry.Weight))
                {
                    return entry.Item;
                }

                minValue += entry.Weight;
            }
            // edge case: roll hit the top of the table.
            return tableEntries[tableEntries.Count - 1].Item;
        }

        private static bool InRange(float val, float min, float max)
        {
            return val >= min && val < max;
        }

    }
}
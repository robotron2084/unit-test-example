using System.Collections;
using System.Collections.Generic;
using System.Linq;
using com.enemyhideout.utils;
using UnityEngine;

namespace com.enemyhideout.loot
{
    /// <summary>
    /// Shell class that wraps the loot core. Rolls for a new item every one second.
    /// </summary>
    public class LootDropper : MonoBehaviour
    {
        /// <summary>
        /// A list of loot tables to use as data.
        /// </summary>
        public List<LootTableData> lootTables;
        
        /// <summary>
        /// The id of the specific table to user.
        /// </summary>
        public string tableToUse;

        /// <summary>
        /// Our source of randomness.
        /// </summary>
        private IRandom _random;
        public IEnumerator Start()
        {
            _random = new EnemyRandom();
            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                RollForItem();
            }
        }

        /// <summary>
        /// Gets our 
        /// </summary>
        private void RollForItem()
        {
            LootTableData tableData = TableForId(lootTables, tableToUse);
            string itemDropped = LootCore.RollForItem(tableData.Entries, _random);
            Debug.Log($"Dropped item {itemDropped}.");
        }
        
        public static LootTableData TableForId(IEnumerable<LootTableData> tables, string tableToUse)
        {
            return tables.FirstOrDefault(x => x.Id == tableToUse);
        }

    }
}
using System;
using System.Collections.Generic;


/// <summary>
/// A data object that contains an id for lookup, and a list of loot table entries that comprise potential loot that can be dropped.
/// </summary>
[Serializable]
public class LootTableData
{
    public string Id;
    
    public List<LootTableEntry> Entries;
    
}

/// <summary>
/// An entry for loot within a table.
/// </summary>
[Serializable]
public class LootTableEntry
{
    public float Weight = 1.0f;
    public string Item;
}



using System.Collections.Generic;
using Assets.Code.Editor;
using com.enemyhideout.loot;
using com.enemyhideout.loot.tests;
using NUnit.Framework;

public class LootCoreTests
{
    
    /// <summary>
    /// Unit tests test 'units'. Don't test your entire application, but individual areas.
    /// </summary>
    /// <param name="testCase"></param>
    [Test]
    public void TestRollForItem([ValueSource(nameof(RollForItemTestCases))] RollForItemTestCase testCase)
    {
        var notRandom = new NotRandom(testCase.RandomValue);
        var output = LootCore.RollForItem(testCase.Entries, notRandom);
        Assert.That(output, Is.EqualTo(testCase.Expected));
    }

    /// <summary>
    /// Using static data for your tests allows you to re-use the data more within your test cases, in case you need
    /// to update your data.
    /// </summary>

    private static float _defaultWeight = 1.0f;
    private static string _item1 = "Item1";
    private static string _item2 = "Item2";
    private static string _item3 = "Item3";
    
    /// <summary>
    /// Never have your tests use data from your game. Mock up data and always use test data, since real game data
    /// is subject to constant change.
    /// </summary>
    private static List<LootTableEntry> _testEntries1 = new List<LootTableEntry>()
    {
        new LootTableEntry
        {
            Weight = _defaultWeight,
            Item = _item1
        },
        new LootTableEntry
        {
            Weight = _defaultWeight,
            Item = _item2
        },
        new LootTableEntry
        {
            Weight = _defaultWeight,
            Item = _item3
        }
    };

    /// <summary>
    /// Here is a list of our test cases that we'll feed into our the above <see cref="TestRollForItem"/> function.
    /// Each test case has a Description field that is used within the UI to differentiate it from other use cases and
    /// give a much better idea of what its testing.
    /// </summary>
    public static List<RollForItemTestCase> RollForItemTestCases = new List<RollForItemTestCase>()
    {
        new RollForItemTestCase
        {
            Description = "First Item, Low Roll",
            Entries = _testEntries1,
            RandomValue = 0.0f,
            Expected = _item1
        },
        new RollForItemTestCase
        {
            Description = "First Item, High Roll",
            Entries = _testEntries1,
            RandomValue = 0.99999f,
            Expected = _item1
        },
        new RollForItemTestCase
        {
            Description = "Second Item, Low Roll",
            Entries = _testEntries1,
            RandomValue = 1.0f,
            Expected = _item2
        },
        new RollForItemTestCase
        {
            Description = "Second Item, High Roll",
            Entries = _testEntries1,
            RandomValue = 1.9999f,
            Expected = _item2
        },
        new RollForItemTestCase
        {
            Description = "Last Item",
            Entries = _testEntries1,
            RandomValue = 2.0f,
            Expected = _item3
        },
        new RollForItemTestCase
        {
            Description = "Last Item, Near High Roll",
            Entries = _testEntries1,
            RandomValue = 2.99999f,
            Expected = _item3
        },
        new RollForItemTestCase
        {
            Description = "Last Item, High Roll",
            Entries = _testEntries1,
            RandomValue = 3.0f,
            Expected = _item3
        }
    };
}

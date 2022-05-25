# Unit testing in Unity

This is an article-as-a-project example of my general setup for unit testing within Unity. It shows a few of what I consider best practices for testing within Unity. The target audience is intermediate-level developers and does not go into the basics of unit testing. For more beginner information I recommend checking out [Unity's Getting Started page.](https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/getting-started.html)

## Testing in Game Development
Testing in games is quite frequently something that is never done, or done after the fact. Why is it not common to test games in development? I think a lot of this comes down to not understanding *how* to test. But frequently code is written in such a way that makes it very difficult to test.

## Reasons For Testing In Games
Long term, if you want your game to be stable, it is wise to include some automated testing. Automated testing can help you avoid things like:
 * Really hard to test bugs that occur in weird edge cases.
 * Corrupted save files due to unexpected 
 * Game play logic that 'usually works' but sometimes just...doesn't?
 * Need to rewrite a big chunk of code and ensure that the new system works identical to the old.
 * The #1 Reason: _Fixing one thing...and breaking another!_

## Unit Testing Vs. Other Tests
In the past I've used testing to test rules engines for games. When those games had issues, and a bug was created, I'd create a new test with the bug number and add it to the suite. I've also used tests in a 'bot' fashion to play through a game and find unsolvable areas. I've also used them to ensure that things like web requests worked as I expected them to.

But really? Honestly? These were not good tests. These tests could have been better. The #1 issue with writing tests is _maintainability_. Noone wants to spend a lot of time keeping tests up to date as things change. Most of these tests suffered from requiring too much effort to maintain. Sometimes they were just abandoned and forgotten about. And the main issue? They were not *unit* tests. 

Unit tests test one thing aka a 'unit', such as a function. Not a big function, mind you, a small one, with ALL potential types of inputs and outputs.

In this example project we test the following unit:

```csharp
public static string RollForItem(List<LootTableEntry> tableEntries, IRandom random)
{
    ...
}
```

If we want to test it, what are all the cases we need to cover?
  * What happens if we pass null into this function?
  * What happens if `tableEntries` is empty/has one/has many items?
  * What happens if the `weight` is zero?


TODO: Unit testing vs. integration tests, big tests, and view tests. What is a good test and what is a bad test?

## The Other Big Problem: Dependencies

While I talk more about this in my [Functional Core/Imperative Shell article](http://enemyhideout.com/2022/04/taming-the-code-jungle-fc-is-in-game-development/), the other issue with testing is how you write the code. Writing testable code means writing code that is small and has a clear goal. Consider this probably all too real looking mono behaviour:

```csharp
public MyMonoBehaviour : MonoBehaviour
{
    public FxManager fxManager;
    public PathingManager pathManager;
    public AnimationUtility animUtility;
    
    ...

    public void KillShot(Unit enemyUnit)
    {
        fxManager.PlaySplosions();
        pathManager.PushBackUnit(enemyUnit);
    }
    
    ...
} 
```

If you wanted to test something like this, how would you? You need an FXManager, PathingManager, and an AnimationUtility. Maybe you don't need an AnimationUtility, but you have to initialize one anyway, even tho its unrelated to what you want to test. What we need is for our code to be isolated, with only the data it needs. But really, it would be mad to test code like this. What we really want to do is test the functional core of this code. This code is shell code, and should not be tested.

## MonoBehaviour Abuse

This also illustrates another issue with testing in Unity: Monobehaviours are not very testable. Most of your game code should not be in a `MonoBehaviour`(or `ScriptableObject`). This is one of the biggest problems with most Unity tutorials. You can see how the structure of this code is such that the `LootDropper` monobehaviour is a shell class that calls into `LootCore` which does the actual work.

```csharp
private void RollForItem()
{
    LootTableData tableData = TableForId(lootTables, tableToUse);
    string itemDropped = LootCore.RollForItem(tableData.Entries, _random);
    Debug.Log($"Dropped item {itemDropped}.");
}
```

## Testing Setup
TODO:
 ## Using Assembly References
When setting up tests, its important to use Assemblies (aka Assembly Definitions) for your tests. I also use them for my code, which is less important, but in general a good practice. 

You can create a test assembly via the `Create->Testing->Tests Assembly Folder`
![Assembly Overview](images/right_click_create.png)

You can then also create a test script within that folder via Create C# Test Script. This will give you some basic example code. But we'll ignore that.

The test class we'll be making looks more like:
```csharp
...
using NUnit.Framework;

public class LootCoreTests
{
    [Test]
    public void TestRollForItem([ValueSource(nameof(RollForItemTestCases))] RollForItemTestCase testCase)
    {
        ...
    }
}
```

![Assembly Overview](images/test_assembly_overview.png)

Tests can either be Editor tests or Play Mode tests, and it is unintuitive how to correctly set up your tests. I have never made a test that was for Play Mode, and I feel like if you are making Play Mode tests, you are not doing unit tests. Avoid setting up Play Mode Tests by ensuring your testing assembly is set to only compile for the editor (which is how unity knows its an Editor only test).

![Assembly Only Compiles in Editor](images/test_assembly_for_editor.png)
 * Set up AssemblyInfo.cs with internal flags and using `internal` to expose tests to.
 * Setting up your tests to run fast.
 * The TestRunner Interface


## ValueSource and Test Cases
TODO: Talk about how to structure tests by using a test case and ValueSourceAttribute

https://docs.nunit.org/articles/nunit/writing-tests/attributes/valuesource.html

## Mocks and Stubs
TODO: Talk about mocks and stubs and how to avoid them.

## Using FC/IS to write testable code.
TODO: Explain how the FC/IS pattern relates to unit testing.

http://enemyhideout.com/2022/04/taming-the-code-jungle-fc-is-in-game-development/

## Randomness in Tests
TODO: Talk about IRandom and why you should write code this way.

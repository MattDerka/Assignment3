using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Frontend2;
using Frontend2.Hardware;

[TestClass]
public class UnitTest1
{
    [TestMethod]    // Test T01
    public void GoodTestMethod1()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("Coke");
        popNames.Add("water");
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(250);
        popCosts.Add(250);
        popCosts.Add(205);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        //Adding another 25 coin
        twentyFiveCoins.Add(twentyFive);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);

        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");
        PopCan stuff = new PopCan("stuff");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(twentyFive);
        vm.CoinSlot.AddCoin(twentyFive);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();
        checkItems.Add(coke);

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();
        expected25.Add(twentyFive);
        expected25.Add(twentyFive);
        expected25.Add(twentyFive);
        expected25.Add(twentyFive);

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);
        expected100.Add(hundred);


        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedCoke = new List<PopCan>();

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        expected.PopCansInPopCanRacks.Add(expectedCoke);
        expected.PopCansInPopCanRacks.Add(expectedWater);
        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();


        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);

    }

    [TestMethod]    // test T02
    public void GoodTestMethod2()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("Coke");
        popNames.Add("water");
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(250);
        popCosts.Add(250);
        popCosts.Add(205);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        twentyFiveCoins.Add(twentyFive);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);

        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");
        PopCan stuff = new PopCan("stuff");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();
        checkItems.Add(coke);
        checkItems.Add(twentyFive);
        checkItems.Add(twentyFive);


        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);
        expected100.Add(hundred);
        expected100.Add(hundred);


        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedCoke = new List<PopCan>();

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        expected.PopCansInPopCanRacks.Add(expectedCoke);
        expected.PopCansInPopCanRacks.Add(expectedWater);
        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);

    }

    [TestMethod]
    public void GoodTestMethod3()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();

        List<Coin> expectedTen = new List<Coin>();

        List<Coin> expected25 = new List<Coin>();

        List<Coin> expected100 = new List<Coin>();

        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedCoke = new List<PopCan>();

        List<PopCan> expectedWater = new List<PopCan>();

        List<PopCan> expectedStuff = new List<PopCan>();

        expected.PopCansInPopCanRacks.Add(expectedCoke);
        expected.PopCansInPopCanRacks.Add(expectedWater);
        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);
    }

    [TestMethod]
    public void GoodTestMethod4()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("Coke");
        popNames.Add("water");
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(250);
        popCosts.Add(250);
        popCosts.Add(205);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        //Adding another 25 coin
        twentyFiveCoins.Add(twentyFive);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);

        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");
        PopCan stuff = new PopCan("stuff");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();
        expected25.Add(twentyFive);
        expected25.Add(twentyFive);

        List<Coin> expected100 = new List<Coin>();

        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedCoke = new List<PopCan>();
        expectedCoke.Add(coke);

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        expected.PopCansInPopCanRacks.Add(expectedCoke);
        expected.PopCansInPopCanRacks.Add(expectedWater);
        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);
    }
    [TestMethod]
    public void GoodTestMethod5()
    {
        int[] coins = { 100, 5, 25, 10 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 2;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("Coke");
        popNames.Add("water");
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(250);
        popCosts.Add(250);
        popCosts.Add(205);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> hundredCoins = new List<Coin>();

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        //Adding another 25 coin
        twentyFiveCoins.Add(twentyFive);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(hundredCoins);
        coinrack[1].LoadCoins(fiveCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(tenCoins);

        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");
        PopCan stuff = new PopCan("stuff");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        vm.SelectionButtons[0].Press();

        var items2 = vm.DeliveryChute.RemoveItems();
        var itemsAsList2 = new List<IDeliverable>(items2);


        var checkItems2 = new List<IDeliverable>();
        checkItems2.Add(coke);
        checkItems2.Add(twentyFive);
        checkItems2.Add(twentyFive);

        CollectionAssert.AreEqual(itemsAsList2, checkItems2);


        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);
        expected100.Add(hundred);

        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedCoke = new List<PopCan>();

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        expected.PopCansInPopCanRacks.Add(expectedCoke);
        expected.PopCansInPopCanRacks.Add(expectedWater);
        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();
        expectedPCISB.Add(hundred);

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[3]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[0]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);
    }

    [TestMethod]
    public void GoodTestMethod6()
    {
        int[] coins = { 100, 5, 25, 10};

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("Coke");
        popNames.Add("water");
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(250);
        popCosts.Add(250);
        popCosts.Add(205);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> hundredCoins = new List<Coin>();

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        //Adding another 25 coin
        twentyFiveCoins.Add(twentyFive);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(hundredCoins);
        coinrack[1].LoadCoins(fiveCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(tenCoins);

        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");
        PopCan stuff = new PopCan("stuff");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();
        expected25.Add(twentyFive);
        expected25.Add(twentyFive);

        List<Coin> expected100 = new List<Coin>();


        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedCoke = new List<PopCan>();
        expectedCoke.Add(coke);

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        expected.PopCansInPopCanRacks.Add(expectedCoke);
        expected.PopCansInPopCanRacks.Add(expectedWater);
        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[3]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[0]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);
    }

    [TestMethod]
    public void GoodTestMethod7()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("A");
        popNames.Add("B");
        popNames.Add("C");

        List<int> popCosts = new List<int>();
        popCosts.Add(5);
        popCosts.Add(10);
        popCosts.Add(25);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        //Adding another 25 coin
        twentyFiveCoins.Add(twentyFive);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);

        PopCan A = new PopCan("A");
        PopCan B = new PopCan("B");
        PopCan C = new PopCan("C");

        List<PopCan> aS = new List<PopCan>();
        aS.Add(A);

        List<PopCan> bS = new List<PopCan>();
        bS.Add(B);

        List<PopCan> cS = new List<PopCan>();
        cS.Add(C);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(aS);
        popCanRack[1].LoadPops(bS);
        popCanRack[2].LoadPops(cS);

        List<string> popNames2 = new List<string>();
        popNames2.Add("Coke");
        popNames2.Add("water");
        popNames2.Add("stuff");

        List<int> popCosts2 = new List<int>();
        popCosts2.Add(250);
        popCosts2.Add(250);
        popCosts2.Add(205);

        vm.Configure(popNames2, popCosts2);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        vm.SelectionButtons[0].Press();

        var items2 = vm.DeliveryChute.RemoveItems();
        var itemsAsList2 = new List<IDeliverable>(items2);

        var checkItems2 = new List<IDeliverable>();
        checkItems2.Add(A);
        checkItems2.Add(twentyFive);
        checkItems2.Add(twentyFive);

        CollectionAssert.AreEqual(itemsAsList2, checkItems2);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);
        expected100.Add(hundred);
        expected100.Add(hundred);

        expected.CoinsInCoinRacks.Add(expectedFive);
        expected.CoinsInCoinRacks.Add(expectedTen);
        expected.CoinsInCoinRacks.Add(expected25);
        expected.CoinsInCoinRacks.Add(expected100);

        List<PopCan> expectedA = new List<PopCan>();

        List<PopCan> expectedB = new List<PopCan>();
        expectedB.Add(B);

        List<PopCan> expectedC = new List<PopCan>();
        expectedC.Add(C);

        expected.PopCansInPopCanRacks.Add(expectedA);
        expected.PopCansInPopCanRacks.Add(expectedB);
        expected.PopCansInPopCanRacks.Add(expectedC);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedA, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedB, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedC, storedContents.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);

        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);

        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");
        PopCan stuff = new PopCan("stuff");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        vm.SelectionButtons[0].Press();

        var items3 = vm.DeliveryChute.RemoveItems();
        var itemsAsList3 = new List<IDeliverable>(items3);

        var checkItems3 = new List<IDeliverable>();
        checkItems3.Add(coke);
        checkItems3.Add(twentyFive);
        checkItems3.Add(twentyFive);

        CollectionAssert.AreEqual(itemsAsList3, checkItems3);

        var storedContents2 = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents2.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra2 = vm.StorageBin.Unload();
        foreach (Coin i in extra2)
        {
            storedContents2.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents2.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected2 = new VendingMachineStoredContents();

        List<Coin> expectedFive_1 = new List<Coin>();
        expectedFive_1.Add(five);

        List<Coin> expectedTen_1 = new List<Coin>();
        expectedTen_1.Add(ten);

        List<Coin> expected25_1 = new List<Coin>();

        List<Coin> expected100_1 = new List<Coin>();
        expected100_1.Add(hundred);
        expected100_1.Add(hundred);
        expected100_1.Add(hundred);

        expected2.CoinsInCoinRacks.Add(expectedFive_1);
        expected2.CoinsInCoinRacks.Add(expectedTen_1);
        expected2.CoinsInCoinRacks.Add(expected25_1);
        expected2.CoinsInCoinRacks.Add(expected100_1);

        List<PopCan> expectedCoke = new List<PopCan>();

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        expected2.PopCansInPopCanRacks.Add(expectedCoke);
        expected2.PopCansInPopCanRacks.Add(expectedWater);
        expected2.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB_1 = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive_1, storedContents2.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen_1, storedContents2.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25_1, storedContents2.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100_1, storedContents2.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedCoke, storedContents2.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents2.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents2.PopCansInPopCanRacks[2]);
        CollectionAssert.AreEqual(expectedPCISB_1, storedContents2.PaymentCoinsInStorageBin);
    }

    [TestMethod]
    public void GoodTestMethod8()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 1;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(140);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);

        List<Coin> hundredCoins = new List<Coin>();
        hundredCoins.Add(hundred);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(hundredCoins);

        PopCan stuff = new PopCan("stuff");

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(stuffs);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();
        checkItems.Add(stuff);
        checkItems.Add(hundred);
        checkItems.Add(twentyFive);
        checkItems.Add(ten);
        checkItems.Add(ten);
        checkItems.Add(ten);

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);
        expected100.Add(hundred);
        expected100.Add(hundred);

        List<PopCan> expectedStuff = new List<PopCan>();

        expected.PopCansInPopCanRacks.Add(expectedStuff);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);


    }
    [TestMethod]
    public void GoodTestMethod9()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 1;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(140);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);

        List<Coin> hundredCoins = new List<Coin>();
        hundredCoins.Add(hundred);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(hundredCoins);

        PopCan stuff = new PopCan("stuff");

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(stuffs);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();
        checkItems.Add(stuff);
        checkItems.Add(hundred);
        checkItems.Add(twentyFive);
        checkItems.Add(ten);
        checkItems.Add(ten);
        checkItems.Add(ten);
        checkItems.Add(five);

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        List<Coin> expectedFive = new List<Coin>();

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);
        expectedTen.Add(ten);
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);
        expected100.Add(hundred);
        expected100.Add(hundred);

        List<PopCan> expectedStuff = new List<PopCan>();
        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);

    }
    [TestMethod]
    public void GoodTestMethod10()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 1;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(140);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);

        List<Coin> hundredCoins = new List<Coin>();
        hundredCoins.Add(hundred);

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(fiveCoins);
        coinrack[1].LoadCoins(tenCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(hundredCoins);

        PopCan stuff = new PopCan("stuff");

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(stuffs);
        Coin one = new Coin(1);
        Coin one39 = new Coin(139);

        vm.CoinSlot.AddCoin(one);
        vm.CoinSlot.AddCoin(one39);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);

        var checkItems = new List<IDeliverable>();
        checkItems.Add(one);
        checkItems.Add(one39);

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);
        expectedTen.Add(ten);
        expectedTen.Add(ten);
        expectedTen.Add(ten);
        expectedTen.Add(ten);
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();
        expected25.Add(twentyFive);

        List<Coin> expected100 = new List<Coin>();
        expected100.Add(hundred);

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[0]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[3]);

        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);

    }
    [TestMethod]
    public void GoodTestMethod11()
    {
        int[] coins = { 100, 5, 25, 10 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml = new VendingMachineLogic(vm);

        List<string> popNames = new List<string>();
        popNames.Add("Coke");
        popNames.Add("water");
        popNames.Add("stuff");

        List<int> popCosts = new List<int>();
        popCosts.Add(250);
        popCosts.Add(250);
        popCosts.Add(205);

        vm.Configure(popNames, popCosts);

        Coin five = new Coin(5);
        Coin ten = new Coin(10);
        Coin twentyFive = new Coin(25);
        Coin hundred = new Coin(100);

        List<Coin> fiveCoins = new List<Coin>();
        fiveCoins.Add(five);

        List<Coin> tenCoins = new List<Coin>();
        tenCoins.Add(ten);

        List<Coin> twentyFiveCoins = new List<Coin>();
        twentyFiveCoins.Add(twentyFive);
        twentyFiveCoins.Add(twentyFive);

        List<Coin> hundredCoins = new List<Coin>();

        var coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(hundredCoins);
        coinrack[1].LoadCoins(fiveCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(tenCoins);

        PopCan stuff = new PopCan("stuff");
        PopCan coke = new PopCan("Coke");
        PopCan water = new PopCan("water");

        List<PopCan> cokes = new List<PopCan>();
        cokes.Add(coke);

        List<PopCan> waters = new List<PopCan>();
        waters.Add(water);

        List<PopCan> stuffs = new List<PopCan>();
        stuffs.Add(stuff);

        var popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.SelectionButtons[0].Press();

        var items = vm.DeliveryChute.RemoveItems();
        var itemsAsList = new List<IDeliverable>(items);
        var checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);
        vm.CoinSlot.AddCoin(hundred);

        items = vm.DeliveryChute.RemoveItems();
        itemsAsList = new List<IDeliverable>(items);
        checkItems = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList, checkItems);

        var storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        VendingMachineStoredContents expected = new VendingMachineStoredContents();

        List<Coin> expectedFive = new List<Coin>();
        expectedFive.Add(five);

        List<Coin> expectedTen = new List<Coin>();
        expectedTen.Add(ten);

        List<Coin> expected25 = new List<Coin>();
        expected25.Add(twentyFive);
        expected25.Add(twentyFive);

        List<Coin> expected100 = new List<Coin>();

        List<PopCan> expectedStuff = new List<PopCan>();
        expectedStuff.Add(stuff);

        List<PopCan> expectedCoke = new List<PopCan>();
        expectedCoke.Add(coke);

        List<PopCan> expectedWater = new List<PopCan>();
        expectedWater.Add(water);

        List<Coin> expectedPCISB = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[3]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[0]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);

        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);
        //CONTINUE FROM LINE 20

        fiveCoins.Clear();
        fiveCoins.Add(five);

        tenCoins.Clear();
        tenCoins.Add(ten);

        twentyFiveCoins.Clear();
        twentyFiveCoins.Add(twentyFive);
        twentyFiveCoins.Add(twentyFive);

        hundredCoins.Clear();

        coinrack = vm.CoinRacks;
        coinrack[0].LoadCoins(hundredCoins);
        coinrack[1].LoadCoins(fiveCoins);
        coinrack[2].LoadCoins(twentyFiveCoins);
        coinrack[3].LoadCoins(tenCoins);

        cokes.Clear();
        cokes.Add(coke);

        waters.Clear();
        waters.Add(water);

        stuffs.Clear();
        stuffs.Add(stuff);

        popCanRack = vm.PopCanRacks;
        popCanRack[0].LoadPops(cokes);
        popCanRack[1].LoadPops(waters);
        popCanRack[2].LoadPops(stuffs);

        vm.SelectionButtons[0].Press();

        items = vm.DeliveryChute.RemoveItems();
        itemsAsList = new List<IDeliverable>(items);
        checkItems = new List<IDeliverable>();
        checkItems.Add(coke);
        checkItems.Add(twentyFive);
        checkItems.Add(twentyFive);

        CollectionAssert.AreEqual(itemsAsList, checkItems);
        //CONTINUE FROM LINE 30
        storedContents = new VendingMachineStoredContents();
        foreach (var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        extra = vm.StorageBin.Unload();
        foreach (Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks in vm.PopCanRacks)
        {
            storedContents.PopCansInPopCanRacks.Add(popCanRacks.Unload());
        }

        expected = new VendingMachineStoredContents();

        expectedFive.Clear();
        expectedFive.Add(five);

        expectedTen.Clear();
        expectedTen.Add(ten);

        expected25.Clear();

        expected100.Clear();
        expected100.Add(hundred);
        expected100.Add(hundred);
        expected100.Add(hundred);

        expectedStuff.Clear();
        expectedStuff.Add(stuff);

        expectedCoke.Clear();

        expectedWater.Clear();
        expectedWater.Add(water);

        expectedPCISB.Clear();

        CollectionAssert.AreEqual(expectedFive, storedContents.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expectedTen, storedContents.CoinsInCoinRacks[3]);
        CollectionAssert.AreEqual(expected25, storedContents.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected100, storedContents.CoinsInCoinRacks[0]);

        CollectionAssert.AreEqual(expectedCoke, storedContents.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater, storedContents.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff, storedContents.PopCansInPopCanRacks[2]);

        CollectionAssert.AreEqual(expectedPCISB, storedContents.PaymentCoinsInStorageBin);
        //Create the new VM
        VendingMachine vm1 = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic vml1 = new VendingMachineLogic(vm);

        List<string> popNames1 = new List<string>();
        popNames1.Add("Coke");
        popNames1.Add("water");
        popNames1.Add("stuff");

        List<int> popCosts1 = new List<int>();
        popCosts1.Add(250);
        popCosts1.Add(250);
        popCosts1.Add(205);

        vm1.Configure(popNames1, popCosts1);

        List<string> popNames1Change = new List<string>();
        popNames1Change.Add("A");
        popNames1Change.Add("B");
        popNames1Change.Add("C");

        List<int> popCosts1Change = new List<int>();

        popCosts1Change.Add(5);
        popCosts1Change.Add(10);
        popCosts1Change.Add(25);

        vm1.Configure(popNames1Change, popCosts1Change);

        var storedContents1 = new VendingMachineStoredContents();
        foreach (var coinRack1 in vm1.CoinRacks)
        {
            storedContents1.CoinsInCoinRacks.Add(coinRack1.Unload());
        }

        List<Coin> extra1 = vm1.StorageBin.Unload();

        foreach (Coin i in extra1)
        {
            storedContents1.PaymentCoinsInStorageBin.Add(i);
        }

        foreach (var popCanRacks1 in vm1.PopCanRacks)
        {
            storedContents1.PopCansInPopCanRacks.Add(popCanRacks1.Unload());
        }

        VendingMachineStoredContents expected1 = new VendingMachineStoredContents();

        List<Coin> expectedFive1 = new List<Coin>();

        List<Coin> expectedTen1 = new List<Coin>();

        List<Coin> expected251 = new List<Coin>();

        List<Coin> expected1001 = new List<Coin>();

        List<PopCan> expectedStuff1 = new List<PopCan>();

        List<PopCan> expectedCoke1 = new List<PopCan>();

        List<PopCan> expectedWater1 = new List<PopCan>();

        List<Coin> expectedPCISB1 = new List<Coin>();

        CollectionAssert.AreEqual(expectedFive1, storedContents1.CoinsInCoinRacks[1]);
        CollectionAssert.AreEqual(expectedTen1, storedContents1.CoinsInCoinRacks[3]);
        CollectionAssert.AreEqual(expected251, storedContents1.CoinsInCoinRacks[2]);
        CollectionAssert.AreEqual(expected1001, storedContents1.CoinsInCoinRacks[0]);

        CollectionAssert.AreEqual(expectedCoke1, storedContents1.PopCansInPopCanRacks[0]);
        CollectionAssert.AreEqual(expectedWater1, storedContents1.PopCansInPopCanRacks[1]);
        CollectionAssert.AreEqual(expectedStuff1, storedContents1.PopCansInPopCanRacks[2]);

        CollectionAssert.AreEqual(expectedPCISB1, storedContents1.PaymentCoinsInStorageBin);

        List<Coin> fiveCoins1 = new List<Coin>();
        fiveCoins1.Add(five);

        List<Coin> tenCoins1 = new List<Coin>();
        tenCoins1.Add(ten);

        List<Coin> twentyFiveCoins1 = new List<Coin>();
        twentyFiveCoins1.Add(twentyFive);
        twentyFiveCoins1.Add(twentyFive);

        List<Coin> hundredCoins1 = new List<Coin>();

        var coinrack1 = vm1.CoinRacks;
        coinrack1[0].LoadCoins(hundredCoins1);
        coinrack1[1].LoadCoins(fiveCoins1);
        coinrack1[2].LoadCoins(twentyFiveCoins1);
        coinrack1[3].LoadCoins(tenCoins1);

        PopCan popa = new PopCan("A");
        PopCan popb = new PopCan("B");
        PopCan popc = new PopCan("C");

        List<PopCan> apop = new List<PopCan>();
        apop.Add(popa);

        List<PopCan> bpop = new List<PopCan>();
        bpop.Add(popb);

        List<PopCan> cpop = new List<PopCan>();
        cpop.Add(popc);

        var popCanRack1 = vm1.PopCanRacks;
        popCanRack1[0].LoadPops(apop);
        popCanRack1[1].LoadPops(bpop);
        popCanRack1[2].LoadPops(cpop);

        vm1.CoinSlot.AddCoin(ten);
        vm1.CoinSlot.AddCoin(five);
        vm1.CoinSlot.AddCoin(ten);

        vm1.SelectionButtons[2].Press();

        var items1 = vm1.DeliveryChute.RemoveItems();
        var itemsAsList1 = new List<IDeliverable>(items1);
        var checkItems1 = new List<IDeliverable>();

        CollectionAssert.AreEqual(itemsAsList1, checkItems1);

    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]
    public void BadTestMethod5()
    {
        int[] coins = {0};

        int selectionButtonCount = 1;
        int coinRackCapacity = 10;
        int popRackCapacity = 10;
        int receptacleCapacity = 10;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void BadTestMethod6()
    {
        int[] coins = {5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 5;
        int popRackCapacity = 5;
        int receptacleCapacity = 5;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        vm.SelectionButtons[3].Press();
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void BadTestMethod7()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 5;
        int popRackCapacity = 5;
        int receptacleCapacity = 5;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        vm.SelectionButtons[-1].Press();
    }

    [TestMethod]
    [ExpectedException(typeof(IndexOutOfRangeException))]
    public void BadTestMethod8()
    {
        int[] coins = { 5, 10, 25, 100 };

        int selectionButtonCount = 3;
        int coinRackCapacity = 5;
        int popRackCapacity = 5;
        int receptacleCapacity = 5;

        VendingMachine vm = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
        VendingMachineLogic l = new VendingMachineLogic(vm);

        vm.SelectionButtons[4].Press();
    }
}

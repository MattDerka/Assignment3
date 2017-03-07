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

        var items2 = vm.DeliveryChute.RemoveItems();
        var itemsAsList2 = new List<IDeliverable>(items2);
        var checkItems2 = new List<IDeliverable>();

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

        List<Coin> fiveCoins2 = new List<Coin>();
        fiveCoins2.Add(five);

        List<Coin> tenCoins2 = new List<Coin>();
        tenCoins2.Add(ten);

        List<Coin> twentyFiveCoins2 = new List<Coin>();
        twentyFiveCoins2.Add(twentyFive);
        twentyFiveCoins2.Add(twentyFive);

        List<Coin> hundredCoins2 = new List<Coin>();

        var coinrack2 = vm.CoinRacks;
        coinrack2[0].LoadCoins(hundredCoins);
        coinrack2[1].LoadCoins(fiveCoins);
        coinrack2[2].LoadCoins(twentyFiveCoins);
        coinrack2[3].LoadCoins(tenCoins);

        List<PopCan> cokes2 = new List<PopCan>();
        cokes2.Add(coke);

        List<PopCan> waters2 = new List<PopCan>();
        waters2.Add(water);

        List<PopCan> stuffs2 = new List<PopCan>();
        stuffs2.Add(stuff);

        var popCanRack2 = vm.PopCanRacks;
        popCanRack2[0].LoadPops(cokes);
        popCanRack2[1].LoadPops(waters);
        popCanRack2[2].LoadPops(stuffs);

        vm.SelectionButtons[0].Press();

        var items3 = vm.DeliveryChute.RemoveItems();
        var itemsAsList3 = new List<IDeliverable>(items3);
        var checkItems3 = new List<IDeliverable>();
        checkItems3.Add(coke);
        checkItems3.Add(twentyFive);
        checkItems3.Add(twentyFive);

        CollectionAssert.AreEqual(itemsAsList3, checkItems3);
        //CONTINUE FROM LINE 30
    }
}

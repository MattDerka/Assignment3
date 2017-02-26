using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Frontend2;
using Frontend2.Hardware;

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]    // Test T01
        public void TestMethod1()
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

        var checkItem = itemsAsList[0];

        var storedContents = new VendingMachineStoredContents();
        foreach(var coinRack in vm.CoinRacks)
        {
            storedContents.CoinsInCoinRacks.Add(coinRack.Unload());
        }

        List<Coin> extra = vm.StorageBin.Unload();
        foreach(Coin i in extra)
        {
            storedContents.PaymentCoinsInStorageBin.Add(i);
        }
    
        foreach(var popCanRacks in vm.PopCanRacks)
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


        Assert.ReferenceEquals(expected,storedContents);
        
        }

        [TestMethod]    // test T02
        public void testMethod2()
        {
            int[] coins = {5, 10, 25, 100 };

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

            //var checkItem = itemsAsList[0];

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


            Assert.ReferenceEquals(expected, storedContents);

    }

    [TestMethod]
    public void testMethod3()
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

        //var checkItem = itemsAsList[0];

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


        Assert.ReferenceEquals(expected, storedContents);


    }

}

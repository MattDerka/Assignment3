using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frontend2.Hardware;
using Frontend2;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
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

            var rack = vm.CoinRacks;
            rack[0].LoadCoins(fiveCoins);
            rack[1].LoadCoins(tenCoins);
            rack[2].LoadCoins(twentyFiveCoins);

            PopCan coke = new PopCan("Coke");
            PopCan water = new PopCan("water");
            PopCan stuff = new PopCan("stuff");

            List<PopCan> cokes = new List<PopCan>();
            cokes.Add(coke);

            List<PopCan> waters = new List<PopCan>();
            waters.Add(water);

            List<PopCan> stuffs = new List<PopCan>();
            stuffs.Add(stuff);
        }
    }
}

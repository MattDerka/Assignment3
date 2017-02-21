﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frontend2.Hardware;
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

            int a = 0;

            VendingMachine d = new VendingMachine(coins, selectionButtonCount, coinRackCapacity, popRackCapacity, receptacleCapacity);
            VendingMachineLogic l = new VendingMachineLogic(d);

            List<string> popNames = new List<string>();
            popNames.Add("Coke");
            popNames.Add("water");
            popNames.Add("stuff");

            List<int> popCosts = new List<int>();
            popCosts.Add(250);
            popCosts.Add(250);
            popCosts.Add(205);

            d.Configure(popNames, popCosts);
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Frontend2.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Frontend2.Hardware.Tests
{
    [TestClass()]
    public class VendingMachineTests
    {
        [TestMethod()]
        public void ConfigureTest()
        {
            int[] coins = {5, 10, 25, 100 };

            int buttonCount = 3;
            int coinRackCapacity = 10;
            int popRackCapacity = 10;
            int coinReceptacleCapacity = 10;

            var VendingMachine = new VendingMachine(coins, buttonCount, coinRackCapacity, popRackCapacity, coinReceptacleCapacity);


            Assert.Fail();
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata.Test
{
    [TestClass]
    public class ReturnCoinsTests
    {
        private VendingMachine machine;

        [TestInitialize]
        public void SetupTests()
        {
            machine = new VendingMachine();
        }

        [TestMethod]
        public void ReturnsCoins()
        {
            var myCoins = new List<string>()
            {
                "nickel",
                "nickel",
                "nickel",
                "nickel",
                "dime"
            };
            foreach (var coin in myCoins)
            {
                machine.InsertCoin(coin);
            }
            machine.ReturnCoins();
            CollectionAssert.AreEqual(myCoins, machine.CoinReturn);
            Assert.AreEqual("INSERT COIN", machine.CheckDisplay());
        }
    }
}

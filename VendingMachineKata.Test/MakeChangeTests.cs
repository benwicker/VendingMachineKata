using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata.Test
{
    [TestClass]
    public class MakeChangeTests
    {
        private VendingMachine machine;

        [TestInitialize]
        public void SetupTests()
        {
            machine = new VendingMachine();
        }

        [TestMethod]
        public void TooMuchMoney()
        {
            var expectedChange = new List<string>()
            {
                "dime"
            };

            machine.InsertCoin("quarter");
            machine.InsertCoin("quarter");
            machine.InsertCoin("quarter");
            machine.SelectItem(2);
            Assert.AreEqual("candy", machine.ItemReturn[0]);
            Assert.AreEqual("THANK YOU", machine.CheckDisplay());
            Assert.AreEqual("INSERT COIN", machine.CheckDisplay());
            CollectionAssert.AreEqual(expectedChange, machine.CoinReturn);
        }
    }
}

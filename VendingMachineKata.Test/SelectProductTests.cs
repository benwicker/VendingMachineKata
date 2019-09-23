using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata.Test
{
    [TestClass]
    public class SelectProductTests
    {
        private VendingMachine machine;

        [TestInitialize]
        public void SetupTest()
        {
            machine = new VendingMachine();
        }

        [TestMethod]
        public void EnoughMoney()
        {
            // insert $1.00
            machine.InsertCoin("quarter");
            machine.InsertCoin("quarter");
            machine.InsertCoin("quarter");
            machine.InsertCoin("quarter");
            Assert.AreEqual("$1.00", machine.CheckDisplay());

            machine.SelectItem(0); // cola
            Assert.AreEqual("cola", machine.ItemReturn[0]);
            Assert.AreEqual("THANK YOU", machine.CheckDisplay());
            Assert.AreEqual("INSERT COIN", machine.CheckDisplay());

            // shouldn't be able to get any coins returned
            Assert.AreEqual(0, machine.Bank.Count);
        }

        [TestMethod]
        public void NotEnoughMoney()
        {
            machine.SelectItem(0); // cola
            Assert.AreEqual("PRICE $1.00", machine.CheckDisplay());
            Assert.AreEqual("INSERT COIN", machine.CheckDisplay());
        }
    }
}

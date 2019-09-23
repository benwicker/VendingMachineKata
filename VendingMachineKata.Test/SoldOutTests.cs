using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata.Test
{
    [TestClass]
    public class SoldOutTests
    {
        private VendingMachine machine;

        [TestInitialize]
        public void SetupTests()
        {
            var options = new List<VendItem>()
            {
                new VendItem("chips", 50, 0)
            };

            machine = new VendingMachine(options);
        }


        [TestMethod]
        public void ItemSoldOut()
        {
            machine.InsertCoin("quarter");
            machine.InsertCoin("quarter");
            machine.SelectItem(0);

            Assert.AreEqual("SOLD OUT", machine.CheckDisplay());
            Assert.AreEqual("$0.50", machine.CheckDisplay());
            Assert.AreEqual(0, machine.ItemReturn.Count);
        }
    }
}

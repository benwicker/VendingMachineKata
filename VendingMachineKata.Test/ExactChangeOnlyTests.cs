using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachineKata.Test
{
    [TestClass]
    public class ExactChangeOnlyTests
    {
        private VendingMachine machine;

        [TestInitialize]
        public void SetupTests()
        {
            machine = new VendingMachine();
        }

        [TestMethod]
        public void ShowExactChange()
        {
            machine.Nickels = 0;
            machine.Dimes = 0;
            Assert.AreEqual("EXACT CHANGE ONLY", machine.CheckDisplay());
        }

        [TestMethod]
        public void ExactRequiredButNotGiven()
        {
            var expectedReturn = new List<string>()
            {
                "quarter",
                "quarter",
                "quarter",
            };
            machine.Nickels = 0;
            machine.Dimes = 0;
            foreach (var coin in expectedReturn)
            {
                machine.InsertCoin(coin);
            }
            machine.SelectItem(2);
            Assert.AreEqual("EXACT CHANGE ONLY", machine.CheckDisplay());
            CollectionAssert.AreEqual(expectedReturn, machine.CoinReturn);
            Assert.AreEqual(0, machine.ItemReturn.Count);
            
        }
    }
}

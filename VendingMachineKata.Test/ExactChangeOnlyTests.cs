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
            machine.Quarters = 0;
            Assert.AreEqual("EXACT CHANGE ONLY", machine.CheckDisplay());
        }
    }
}

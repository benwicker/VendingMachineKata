using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace VendingMachineKata.Test
{
    [TestClass]
    public class AcceptCoinsTests
    {
        private VendingMachine machine;

        [TestInitialize]
        public void SetupTest()
        {
            machine = new VendingMachine();
        }

        [TestMethod]
        public void ValidCoins()
        {
            Assert.AreEqual("INSERT COIN", machine.CheckDisplay());

            machine.InsertCoin("nickel");
            Assert.AreEqual("$0.05", machine.CheckDisplay());

            machine.InsertCoin("dime");
            Assert.AreEqual("$0.15", machine.CheckDisplay());

            machine.InsertCoin("quarter");
            Assert.AreEqual("$0.40", machine.CheckDisplay());
        }

        [TestMethod]
        public void InvalidCoin()
        {
            machine.InsertCoin("penny");
            Assert.AreEqual("INSERT COIN", machine.CheckDisplay());
            Assert.AreEqual("penny", machine.CoinReturn[0]);
        }
    }
}

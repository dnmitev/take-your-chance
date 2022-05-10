using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class CommandTests
    {
        [TestMethod]
        [DataRow("bet", "12.4")]
        [DataRow("deposit", "12.4")]
        [DataRow("withdraw", "12.4")]
        public void ToString_Should_Return_Given_Command(string action, string amount)
        {
            var cmd = new Command(action, decimal.Parse(amount));
            Assert.AreEqual($"{action} {amount}", cmd.ToString());
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class PlayerTests
    {

        [TestMethod]
        public void DepositToWallet_Should_Add_Amount_Correctly()
        {
            var player = new Player();
            player.DepositToWallet(10);

            Assert.AreEqual(10, player.Balance);

            player.DepositToWallet(10);
            Assert.AreEqual(20, player.Balance);

            player.DepositToWallet(5);
            Assert.AreEqual(25, player.Balance);
        }

        [TestMethod]
        public void GetBalance_Should_Return_Correctly()
        {
            var player = new Player();
            player.DepositToWallet(10);

            Assert.AreEqual(10, player.GetBalance());

            player.DepositToWallet(10);
            Assert.AreEqual(20, player.GetBalance());

            player.DepositToWallet(5);
            Assert.AreEqual(25, player.GetBalance());
        }

        [TestMethod]
        public void DepositToWallet_Should_Fail_When_Negative_Amount_Given()
        {
            var player = new Player();

            Assert.ThrowsException<ArgumentException>(() => player.DepositToWallet(-10));
        }

        [TestMethod]
        public void DepositToWallet_Should_Fail_With_Meaningfull_Message_When_Negative_Amount_Given()
        {
            var player = new Player();
            Assert.ThrowsException<ArgumentException>(() => player.DepositToWallet(-10), $"The deposited amount cannot be below 0. Given amount is -10");
        }
    }
}
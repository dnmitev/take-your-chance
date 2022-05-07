using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class PlayerTests
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Player _player;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        [TestInitialize]
        public void Setup()
        {
            _player = new Player();
        }

        [TestMethod]
        public void DepositToWallet_Should_Add_Amount_Correctly()
        {
            var player = new Player();
            _player.DepositToWallet(10);

            Assert.AreEqual(10, _player.Balance);

            _player.DepositToWallet(10);
            Assert.AreEqual(20, _player.Balance);

            _player.DepositToWallet(5);
            Assert.AreEqual(25, _player.Balance);
        }

        [TestMethod]
        public void GetBalance_Should_Return_Correctly()
        {
            _player.DepositToWallet(10);
            Assert.AreEqual(10, _player.GetBalance());
            Assert.AreEqual(_player.Balance, _player.GetBalance());

            _player.DepositToWallet(10);
            Assert.AreEqual(20, _player.GetBalance());
            Assert.AreEqual(_player.Balance, _player.GetBalance());


            _player.DepositToWallet(5);
            Assert.AreEqual(25, _player.GetBalance());
            Assert.AreEqual(_player.Balance, _player.GetBalance());
        }

        [TestMethod]
        public void DepositToWallet_Should_Fail_When_Negative_Amount_Given()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _player.DepositToWallet(-10));
        }

        [TestMethod]
        public void DepositToWallet_Should_Fail_With_Meaningfull_Message_When_Negative_Amount_Given()
        {
            Assert.ThrowsException<ArgumentException>(
                () => _player.DepositToWallet(-10), $"The deposited amount cannot be below 0. Given amount is -10");
        }

        [TestMethod]
        public void Withdraw_Should_Work_Correctly()
        {
            _player.DepositToWallet(25);
            _player.Withdraw(10);

            Assert.AreEqual(15, _player.GetBalance());
        }

        [TestMethod]
        public void Withdraw_Should_Fail_When_Given_Amount_Is_Negative()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _player.Withdraw(-10), $"The withdrawing amont cannot be below 0. Given amount: -10");
        }

        [TestMethod]
        public void Withdraw_Should_Fail_When_Amount_Is_Greater_Then_Current_Balance()
        {
            _player.DepositToWallet(100);
            Assert.ThrowsException<ArgumentException>(
                () => _player.Withdraw(200), $"The given amount (200) is more than the wallet's balance (100)");
        }
    }
}
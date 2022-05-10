using ConsoleRoyale.RNG;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ConsoleRoyale.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        private const decimal DEFAULT_BET = 5;
        private const int DEFAULT_ITERATIONS = int.MaxValue;

        [TestMethod]
        public void Playing_Infinite_Games_Should_Throw_An_Exception_Due_To_Insuffucient_Amount_In_Wallet()
        {
            //// Playing an infinite count of games should be in favor of the casino because
            //// of the loosing rate. Therefore a small bet played N-times should rusult into a wallet 
            //// below the given bet.
            var player = new Player(100000);
            var rng = new RNGProvider();
            var game = new Game(player, rng);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                for (int i = 0; i < DEFAULT_ITERATIONS; i++)
                {
                    var outcome = game.Play(DEFAULT_BET);
                }
            });
        }

        [TestMethod]
        public void Playing_Infinite_Games_Should_Get_Expected_Distribution_Of_Wins_And_Loses()
        {
            var initialBalance = (decimal)Math.Pow(10, 8);
            var player = new Player(initialBalance);
            var rng = new RNGProvider();
            var game = new Game(player, rng);

            var x2Count = 0;
            var x10Count = 0;
            var lose = 0;

            var iterations = (int)Math.Pow(10, 6);

            for (int i = 0; i < iterations; i++)
            {
                var outcome = game.Play(1);
                if (outcome == 0)
                {
                    lose++;
                }
                else if (outcome <= 2)
                {
                    x2Count++;
                }
                else
                {
                    x10Count++;
                }
            }


            Assert.IsTrue(x10Count * 100 / iterations <= 10);
            Assert.IsTrue(x2Count * 100 / iterations <= 40);
            Assert.IsTrue(lose * 100 / iterations <= 50);

            Assert.IsTrue(game.Player.GetBalance() < initialBalance);
        }
    }
}
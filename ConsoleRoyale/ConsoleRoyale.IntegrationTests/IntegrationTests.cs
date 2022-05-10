using ConsoleRoyale.RNG;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ConsoleRoyale.IntegrationTests
{
    [TestClass]
    public class IntegrationTests
    {
        private const decimal DEFAULT_BET = 5;
        private const int DEFAULT_ITTERATIONS = int.MaxValue;

        [TestMethod]
        public void Playing_Infinite_Games_Should_Throw_An_Exception_Due_To_Insuffucient_Amount_In_Wallet()
        {
            //// Playing an infinite count of games should be in favor of the casino because
            //// of the loosing rate. Therefore a small bet played N-times should rusult into a wallet 
            //// below the given bet.
            var player = new Player(100000);
            var rng = new RNGProvider();
            var game = new Game(player, rng);
            var cmdProcessor = new SimpleCommandProcessor();
            var retriever = new ConsoleRetriever();
            var output = new ConsoleOutput();
            var consoleGameEngine = new ConsoleGameEngine(game, cmdProcessor, retriever, output);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                for (int i = 0; i < DEFAULT_ITTERATIONS; i++)
                {
                    var outcome = game.Play(DEFAULT_BET);
                }
            });
        }
    }
}
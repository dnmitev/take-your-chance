using ConsoleRoyale.RNG;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class GameTests
    {
        private const int PLAYER_INTIAL_DEPOSIT = 1000;
        private const int PLAYER_DEFAULT_BET = 5;

        private const decimal DEFAULT_MULTIPLIER_UP_TO_X2 = 1.02m;
        private const decimal DEFAULT_MULTIPLIER_UP_TO_X10 = 5.3m;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private IPlayableGame _game;
        private IPlayer _player;
        private Mock<IRNGProvider> _mockRng;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        [TestInitialize]
        public void Setup()
        {
            _player = new Player();
            _player.DepositToWallet(PLAYER_INTIAL_DEPOSIT);

            _mockRng = new Mock<IRNGProvider>();

            _game = new Game(_player, _mockRng.Object);
        }

        [TestMethod]
        public void Play_Should_Return_Win_When_Chance_Below_10()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(1);
            _mockRng.Setup(r => r.GetRandomDoubleInInterval(It.IsAny<int>(), It.IsAny<int>())).Returns(DEFAULT_MULTIPLIER_UP_TO_X10);

            var outcome = _game.Play(PLAYER_DEFAULT_BET);
            Assert.IsTrue(outcome > 0);
        }

        [TestMethod]
        public void Play_Should_Calculate_Win_Correctly_When_Chance_Below_10()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(1);
            _mockRng.Setup(r => r.GetRandomDoubleInInterval(It.IsAny<int>(), It.IsAny<int>())).Returns(DEFAULT_MULTIPLIER_UP_TO_X10);

            var outcome = _game.Play(PLAYER_DEFAULT_BET);
            Assert.AreEqual((DEFAULT_MULTIPLIER_UP_TO_X10 * PLAYER_DEFAULT_BET), outcome);
        }

        [TestMethod]
        public void Play_Should_Get_Multiplier_From_2_10_When_Chance_Below_10()
        {

            for (int i = 1; i < 11; i++)
            {
                _mockRng.Setup(r => r.GetPercentageChance()).Returns(i);
                var outcome = _game.Play(PLAYER_DEFAULT_BET);

                _mockRng.Verify(m => m.GetRandomDoubleInInterval(2, 10));
            }
        }

        [TestMethod]
        public void Play_Should_Not_Get_Multiplier_From_2_10_When_Chance_Above_10()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(11);
            var outcome = _game.Play(PLAYER_DEFAULT_BET);

            _mockRng.Verify(m => m.GetRandomDoubleInInterval(2, 10), Times.Never());
        }

        [TestMethod]
        public void Play_Should_Return_Win_When_Chance_Above_10_Below_50()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(25);
            _mockRng.Setup(r => r.GetRandomDoubleInInterval(It.IsAny<int>(), It.IsAny<int>())).Returns(DEFAULT_MULTIPLIER_UP_TO_X2);

            var outcome = _game.Play(PLAYER_DEFAULT_BET);
            Assert.IsTrue(outcome > 0);
            Assert.AreEqual((DEFAULT_MULTIPLIER_UP_TO_X2 * PLAYER_DEFAULT_BET), outcome);
        }

        [TestMethod]
        public void Play_Should_Calculate_Win_Correctly_When_Chance_Above_10_Below_50()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(25);
            _mockRng.Setup(r => r.GetRandomDoubleInInterval(It.IsAny<int>(), It.IsAny<int>())).Returns(DEFAULT_MULTIPLIER_UP_TO_X2);

            var outcome = _game.Play(PLAYER_DEFAULT_BET);
            Assert.AreEqual((DEFAULT_MULTIPLIER_UP_TO_X2 * PLAYER_DEFAULT_BET), outcome);
        }

        [TestMethod]
        public void Play_Should_Put_Correct_Amount_On_Players_Wallet_When_Win_Up_To_x2()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(25);
            _mockRng.Setup(r => r.GetRandomDoubleInInterval(It.IsAny<int>(), It.IsAny<int>())).Returns(DEFAULT_MULTIPLIER_UP_TO_X2);

            var outcome = _game.Play(PLAYER_DEFAULT_BET);
            Assert.AreEqual((PLAYER_INTIAL_DEPOSIT - PLAYER_DEFAULT_BET + DEFAULT_MULTIPLIER_UP_TO_X2 * PLAYER_DEFAULT_BET), _player.GetBalance());
        }

        [TestMethod]
        public void Play_Should_Put_Correct_Amount_On_Players_Wallet_When_Win_Up_To_x10()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(1);
            _mockRng.Setup(r => r.GetRandomDoubleInInterval(It.IsAny<int>(), It.IsAny<int>())).Returns(DEFAULT_MULTIPLIER_UP_TO_X10);

            var outcome = _game.Play(PLAYER_DEFAULT_BET);
            Assert.AreEqual((PLAYER_INTIAL_DEPOSIT - PLAYER_DEFAULT_BET + DEFAULT_MULTIPLIER_UP_TO_X10 * PLAYER_DEFAULT_BET), _player.GetBalance());
        }

        [TestMethod]
        public void Play_Should_Get_Multiplier_From_1_2_When_Chance_Above_10_Below_50()
        {

            for (int i = 11; i < 51; i++)
            {
                _mockRng.Setup(r => r.GetPercentageChance()).Returns(i);
                var outcome = _game.Play(1);

                _mockRng.Verify(m => m.GetRandomDoubleInInterval(1, 2));
            }
        }

        [TestMethod]
        public void Play_Should_Not_Get_Multiplier_From_1_2_When_Chance_Above_50()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(51);
            var outcome = _game.Play(PLAYER_DEFAULT_BET);

            _mockRng.Verify(m => m.GetRandomDoubleInInterval(1, 2), Times.Never());
        }

        [TestMethod]
        public void Play_Should_Lose_When_Chance_Above_50()
        {
            for (int i = 50; i < 101; i++)
            {
                _mockRng.Setup(r => r.GetPercentageChance()).Returns(i);
                var outcome = _game.Play(PLAYER_DEFAULT_BET);

                Assert.AreEqual(0, outcome);
            }
        }

        [TestMethod]
        public void Play_Should_Take_Bet_From_Player_Wallet()
        {
            _mockRng.Setup(r => r.GetPercentageChance()).Returns(78);
            var outcome = _game.Play(PLAYER_DEFAULT_BET);

            Assert.AreEqual(PLAYER_INTIAL_DEPOSIT - PLAYER_DEFAULT_BET + outcome, _player.GetBalance());
        }
    }
}

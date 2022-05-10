using ConsoleRoyale.RNG;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class ConsoleGameEngineTests
    {
        private IGameEngine _engine;

        private Mock<IPlayableGame> _mockGame;
        private Mock<IPlayer> _mockPlayer;
        private Mock<IRNGProvider> _mockRng;

        private Mock<ICommandProcessor> _mockCmdProcessor;

        private Mock<IRetriever> _mockRetriever;
        private Mock<IOutput> _mockOutput;

        [TestInitialize]
        public void Setup()
        {
            _mockRng = new Mock<IRNGProvider>();
            _mockCmdProcessor = new Mock<ICommandProcessor>();
            _mockRetriever = new Mock<IRetriever>();
            _mockOutput = new Mock<IOutput>();
            _mockPlayer = new Mock<IPlayer>();
            _mockGame = new Mock<IPlayableGame>();

            _engine = new ConsoleGameEngine(_mockGame.Object, _mockCmdProcessor.Object, _mockRetriever.Object, _mockOutput.Object);
        }

        [TestMethod]
        public void Run_Should_Show_Default_Message()
        {
            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null)).Returns(new Command("exit"));
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);
            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);

            _engine.Run();

            _mockOutput.Verify(m => m.Write("Please submit your action:"), Times.Once);
        }

        [TestMethod]
        public void Run_Should_Get_Input_From_Retriever()
        {
            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null)).Returns(new Command("exit"));
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);
            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);

            _engine.Run();

            _mockRetriever.Verify(m => m.RetrieveInput(), Times.Once);
        }

        [TestMethod]
        [DataRow("exit", "exit", "0")]
        [DataRow("deposit 3.52", "deposit", "3.52")]
        [DataRow("withdraw 3.52", "withdraw", "3.52")]
        [DataRow("bet 3.52", "bet", "3.52")]
        public void Run_Should_Process_The_Input_Command(string input, string action, string amount)
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns(input);

            _mockPlayer.Setup(p => p.DepositToWallet(It.IsAny<decimal>())).Returns(decimal.Parse(amount));
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);

            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);
            _mockGame.Setup(g => g.Play(It.IsAny<decimal>())).Returns(0);

            _mockCmdProcessor.Setup(cmd => cmd.Process(input, null)).Returns(new Command(action, decimal.Parse(amount)));

            _engine.Run();

            _mockCmdProcessor.Verify(cmd => cmd.Process(input, null), Times.Once);
        }

        [TestMethod]
        public void Run_Should_Deposit_When_Deposit_Command()
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns("deposit 3.2");

            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);

            _mockGame.Setup(g => g.Play(It.IsAny<decimal>())).Returns(0);

            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null))
                .Returns(new Command("deposit", 3.2m));

            _engine.Run();

            _mockPlayer.Verify(p => p.DepositToWallet(3.2m), Times.Once);
        }

        [TestMethod]
        public void Run_Should_Withdraw_When_Withdraw_Command()
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns("withdraw 3.2");

            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);

            _mockGame.Setup(g => g.Play(It.IsAny<decimal>())).Returns(0);

            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null))
                .Returns(new Command("withdraw", 3.2m));

            _engine.Run();

            _mockPlayer.Verify(p => p.Withdraw(3.2m), Times.Once);
        }

        [TestMethod]
        public void Run_Should_Play_Game_When_Bet_Command()
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns("bet 3.2");

            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);

            _mockGame.Setup(g => g.Play(It.IsAny<decimal>())).Returns(0);

            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null))
                .Returns(new Command("bet", 3.2m));

            _engine.Run();

            _mockGame.Verify(g => g.Play(3.2m), Times.Once);
        }

        [TestMethod]
        [DataRow("0", "No luck this time! Your current balance is $18.34")]
        [DataRow("1.2", "Congrats! You won $1.20! Your current balance is $18.34")]
        public void Run_Should_Play_Game_When_Bet_Command_And_Output(string outcome, string message)
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns("bet 3.2");

            _mockPlayer.Setup(p => p.ToString()).Returns("18.34");
            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);

            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);
            _mockGame.Setup(g => g.Play(It.IsAny<decimal>())).Returns(decimal.Parse(outcome));

            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null))
                .Returns(new Command("bet", 3.2m));

            _engine.Run();

            _mockOutput.Verify(
                o => o.Write(message), Times.Once);
        }

        [TestMethod]
        public void Run_Should_Continue_On_Unknown_Command()
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns("top-up 3.2");

            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);
            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);

            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null))
                .Returns(new Command("topup", 3.2m));

            var shouldContinue = _engine.Run();

            Assert.IsTrue(shouldContinue);
        }

        [TestMethod]
        public void Run_Should_Cancel_On_Exit_Command()
        {
            _mockOutput.Setup(m => m.Write(It.IsAny<string>()));
            _mockRetriever.Setup(r => r.RetrieveInput()).Returns("exit");

            _mockPlayer.Setup(p => p.GetBalance()).Returns(18.34m);
            _mockGame.Setup(g => g.Player).Returns(_mockPlayer.Object);

            _mockCmdProcessor.Setup(cmd => cmd.Process(It.IsAny<string>(), null))
                .Returns(new Command("exit"));

            var shouldContinue = _engine.Run();

            Assert.IsFalse(shouldContinue);
        }
    }
}

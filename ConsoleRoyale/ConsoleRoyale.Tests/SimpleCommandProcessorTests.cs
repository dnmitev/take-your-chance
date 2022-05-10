using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;

namespace ConsoleRoyale.Tests
{
    [TestClass]
    public class SimpleCommandProcessorTests
    {
        private ICommandProcessor _commandProcessor;

        [TestInitialize]
        public void Setup()
        {
            _commandProcessor = new SimpleCommandProcessor();
        }

        [TestMethod]
        [DataRow("deposit 2.5", "deposit", "2.5")]
        [DataRow("deposit 2", "deposit", "2")]
        [DataRow("deposit 2.54", "deposit", "2.54")]
        [DataRow("withdraw 2.5", "withdraw", "2.5")]
        [DataRow("withdraw 3.12", "withdraw", "3.12")]
        [DataRow("withdraw 2", "withdraw", "2")]
        [DataRow("bet 2.5", "bet", "2.5")]
        [DataRow("bet 2.54", "bet", "2.54")]
        [DataRow("bet 2", "bet", "2")]
        [DataRow("exit", "exit", "0")]
        [DataRow("exit     ", "exit", "0")]
        [DataRow("bet     2   ", "bet", "2")]
        [DataRow("  withdraw   3.12  ", "withdraw", "3.12")]
        public void Process_Should_Parse_Command_Correctly(string input, string action, string amount)
        {
            var output = _commandProcessor.Process(input);

            Assert.IsTrue(output.Action == action);
            Assert.IsTrue(output.Amount == decimal.Parse(amount));
        }

        [TestMethod]
        [DataRow("deposit*2.5", '*', "deposit", "2.5")]
        [DataRow("deposit@2.5", '@', "deposit", "2.5")]
        [DataRow("@deposit@@@@2.5@", '@', "deposit", "2.5")]
        public void Process_Should_Parse_Command_Correctly_When_Delimeter_Given(string input, char delimeter, string action, string amount)
        {
            var output = _commandProcessor.Process(input, delimeter);

            Assert.IsTrue(output.Action == action);
            Assert.IsTrue(output.Amount == decimal.Parse(amount));
        }

        [TestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("          ")]
        public void Process_Should_Throw_ArgumentNullException_On_Null_Or_Empty_Command(string input)
        {
            Assert.ThrowsException<ArgumentNullException>(() => _commandProcessor.Process(input));
        }


        [TestMethod]
        [DataRow("deposit 5.3 now")]
        [DataRow("deposit 4.33 exit 0")]
        public void Process_Should_Throw_InvalidOperationException_On_Null_Or_Empty_Command(string input)
        {
            Assert.ThrowsException<InvalidOperationException>(() => _commandProcessor.Process(input));
        }


        [TestMethod]
        [DataRow("deposit boooooom")]
        [DataRow("deposit @#$@")]
        public void Process_Should_Throw_ArgumentException_On_Non_Number_Second_Param(string input)
        {
            Assert.ThrowsException<ArgumentException>(() => _commandProcessor.Process(input));
        }
    }
}

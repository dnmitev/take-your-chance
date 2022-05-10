namespace ConsoleRoyale
{
    public class ConsoleGameEngine : IGameEngine
    {
        private readonly string ACTION_DEPOSIT = "deposit";
        private readonly string ACTION_BET = "bet";
        private readonly string ACTION_WITHDRAW = "withdraw";
        private readonly string ACTION_EXIT = "exit";

        private readonly string DEFAULT_MESSAGE = "Please submit your action:";

        private const int DEFAULT_LOST_VALUE = 0;

        private IPlayableGame _game;
        private ICommandProcessor _commandProcessor;
        private IRetriever _retriever;
        private IOutput _output;

        public ConsoleGameEngine(
            IPlayableGame game,
            ICommandProcessor cmdProcessor,
            IRetriever retriever,
            IOutput output)
        {
            _game = game;
            _commandProcessor = cmdProcessor;
            _retriever = retriever;
            _output = output;
        }

        /// <summary>
        /// Do a single run of the game engine and provide info whether it should continue or not
        /// </summary>
        /// <returns>A bool to decide to continue or not.</returns>
        public bool Run()
        {           
            _output.Write(DEFAULT_MESSAGE);

            var input = _retriever.RetrieveInput();

            ICommand cmd = _commandProcessor.Process(input);

            if (cmd.Action == ACTION_DEPOSIT)
            {
                _game.Player.DepositToWallet(cmd.Amount);
                _output.Write($"Your deposit of ${cmd.Amount:0.00} was successful. Your current balance is ${_game.Player}");
            }
            else if (cmd.Action == ACTION_BET)
            {
                var outcome = _game.Play(cmd.Amount);

                if (outcome == DEFAULT_LOST_VALUE)
                {
                    _output.Write($"No luck this time! Your current balance is ${_game.Player}");
                }
                else
                {
                    _output.Write($"Congrats! You won ${outcome:0.00}! Your current balance is ${_game.Player}");
                }
            }
            else if (cmd.Action == ACTION_WITHDRAW)
            {
                _game.Player.Withdraw(cmd.Amount);
                _output.Write($"Your withdrawal of ${cmd.Amount:0.00} was successful. Your current balance is ${_game.Player}");
            }
            else if (cmd.Action == ACTION_EXIT)
            {
                // execution should stop
                return false;
            }
            else
            {
                _output.Write("Unknown command! Please put a correct command.");
            }

            return true;
        }
    }
}

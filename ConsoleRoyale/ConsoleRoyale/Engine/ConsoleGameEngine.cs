namespace ConsoleRoyale
{
    public class ConsoleGameEngine : IGameEngine
    {
        private readonly string ACTION_DEPOSIT = "deposit";
        private readonly string ACTION_BET = "bet";
        private readonly string ACTION_WITHDRAW = "withdraw";
        private readonly string ACTION_EXIT = "exit";

        private readonly string DEFAULT_MESSAGE = "PLease submit your action:";

        private const int DEFAULT_LOST_VALUE = 0;

        private IPlayableGame _game;
        private ICommandProcessor _commandProcessor;

        public ConsoleGameEngine(IPlayableGame game, ICommandProcessor cmdProcessor)
        {
            _game = game;
            _commandProcessor = cmdProcessor;
        }

        public void Start()
        {
            while (true)
            {
                Console.WriteLine(DEFAULT_MESSAGE);

                var input = Console.ReadLine();

                ICommand cmd;
                try
                {
                    cmd = _commandProcessor.Process(input);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }

                if (cmd.Action == ACTION_DEPOSIT)
                {
                    _game.Player.DepositToWallet(cmd.Amount);
                }
                else if (cmd.Action == ACTION_BET)
                {
                    var outcome = _game.Play(cmd.Amount);

                    if (outcome == DEFAULT_LOST_VALUE)
                    {
                        Console.WriteLine($"No luck this time! Your current balance is ${_game.Player}");
                    }
                    else
                    {
                        Console.WriteLine($"Congrats! You won ${outcome:0.00}! Your current balance is ${_game.Player}");
                    }
                }
                else if (cmd.Action == ACTION_WITHDRAW)
                {
                    _game.Player.Withdraw(cmd.Amount);
                }
                else if (cmd.Action == ACTION_EXIT)
                {
                    return;
                }
                else
                {
                    Console.WriteLine("Unknown command! Please put a correct command.");
                }
            }
        }
    }
}

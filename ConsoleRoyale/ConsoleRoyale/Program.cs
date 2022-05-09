using ConsoleRoyale.RNG;

namespace ConsoleRoyale
{
    public static class Program
    {
        public static void Main()
        {
            var player = new Player();
            var rng = new RNGProvider();

            while (true)
            {
                Console.WriteLine("PLease submit your action:");
                var action = Console.ReadLine();

                var parsedAction = action?.Split(' ');
                var command = parsedAction[0] ?? string.Empty;
                var amount = decimal.Parse(parsedAction[1]);

                if (command.ToLower() == "deposit")
                {
                    player.DepositToWallet(amount);
                }
                else if (command.ToLower() == "bet")
                {
                    var game = new Game(player, rng);
                    var outcome = game.Play(amount);

                    if (outcome == 0)
                    {
                        Console.WriteLine($"No luck this time! Your current balance is ${player}");
                    }
                    else
                    {
                        Console.WriteLine($"Congrats! You won ${Math.Round(outcome, 2)}! Your current balance is ${player}");
                    }
                }
                else if (command.ToLower() == "withdraw")
                {
                    player.Withdraw(amount);
                }
                else
                {
                    return;
                }
            }
        }
    }
}

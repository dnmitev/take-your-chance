using ConsoleRoyale.RNG;

namespace ConsoleRoyale
{
    public static class Program
    {
        public static void Main()
        {
            var player = new Player();
            var rng = new RNGProvider();
            var game = new Game(player, rng);
            var cmdProcessor = new SimpleCommandProcessor();
            var consoleGameEngine = new ConsoleGameEngine(game, cmdProcessor);

            consoleGameEngine.Start();
        }
    }
}

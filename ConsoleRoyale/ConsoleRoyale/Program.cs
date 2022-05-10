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
            var retriever = new ConsoleRetriever();
            var output = new ConsoleOutput();
            var consoleGameEngine = new ConsoleGameEngine(game, cmdProcessor, retriever, output);

            var shoudlPlayGame = true;
            while (shoudlPlayGame)
            {
                try
                {
                    shoudlPlayGame = consoleGameEngine.Run();
                }
                catch (ArgumentNullException ex)
                {
                    output.Write(ex.Message);
                    continue;
                }
                catch (InvalidOperationException ex)
                {
                    output.Write(ex.Message);
                    continue;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    output.Write(ex.Message);
                    continue;
                }
                catch (ArgumentException ex)
                {
                    output.Write(ex.Message);
                    continue;
                }
            }
        }
    }
}

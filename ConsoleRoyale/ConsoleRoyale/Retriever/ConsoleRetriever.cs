namespace ConsoleRoyale
{
    public class ConsoleRetriever : IRetriever
    {
        public string RetrieveInput() => Console.ReadLine() ?? string.Empty;
    }
}

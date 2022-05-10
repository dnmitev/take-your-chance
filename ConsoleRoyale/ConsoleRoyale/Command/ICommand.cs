namespace ConsoleRoyale
{
    public interface ICommand
    {
        public string Action { get; }

        public decimal Amount { get; }
    }
}

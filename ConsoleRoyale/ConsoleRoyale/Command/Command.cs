namespace ConsoleRoyale
{
    public class Command : ICommand
    {
        public Command(string action)
        {
            Action  = action;
            Amount = 0;
        }

        public Command(string action, decimal amount)
        {
            Action = action;
            Amount = amount;
        }

        public string Action { get; private set; }

        public decimal Amount { get; private set; }

        public override string ToString()
        {
            return $"{Action} {Amount}";
        }
    }
}

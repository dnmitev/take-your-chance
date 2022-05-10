namespace ConsoleRoyale
{
    /// <summary>
    /// Processes simple commands in the format 'action amount', i.e. 'deposit 100'
    /// </summary>
    public class SimpleCommandProcessor : ICommandProcessor
    {
        private readonly char DEFAULT_DELIMETER = ' ';

        public ICommand Process(string input, char? delimeter = null)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentNullException($"The input command CANNOT be null or empty string.");
            }

            var parsedCommand = input.Split(delimeter ?? DEFAULT_DELIMETER, StringSplitOptions.RemoveEmptyEntries);

            if (parsedCommand.Length > 2)
            {
                throw new InvalidOperationException($"The given command: {input} is  in wrong format. It should be 'action amount', i.e. 'bet 3.4'");
            }

            var cmd = parsedCommand[0].ToLower();

            if (parsedCommand.Length == 1)
            {
                return new Command(cmd);
            }

            var value = decimal.Parse(parsedCommand[1]);

            return new Command(cmd,value);
        }
    }
}

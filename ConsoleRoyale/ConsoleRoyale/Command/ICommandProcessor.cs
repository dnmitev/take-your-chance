namespace ConsoleRoyale
{
    /// <summary>
    /// A contract for a command processor
    /// </summary>
    public interface ICommandProcessor
    {
        /// <summary>
        /// Process a simple command with an action and optional amount
        /// </summary>
        /// <param name="input">A simple one row string.</param>
        /// <param name="delimeter">The delimeter by which the string will be separated, i.e. "bet*35" with delimeter of "*" will be "bet" and "35".</param>
        /// <returns>a An instance of ICommand where it has action and the given amount.</returns>
        ICommand Process(string input, char? delimeter = null);
    }
}

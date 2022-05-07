namespace ConsoleRoyale
{
    /// <summary>
    /// Acts a contract of what a player should be able to do. The simplest here is to add money and provide info
    /// about the current balance
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// Provides info about the current balance for a given player
        /// </summary>
        /// <returns>The amount of money available in the player's waller</returns>
        decimal GetBalance();

        /// <summary>
        /// Deposit(add) money to player's waller
        /// </summary>
        /// <param name="amount">An amount that should be added to the player's wallet.</param>
        /// <returns>The new amount available after the addition.</returns>
        decimal DepositToWallet(decimal amount);

        /// <summary>
        /// Withdraw given amount from the player's waller
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
        /// <returns>The amount after the withdrawal.</returns>
        decimal Withdraw(decimal amount);
    }
}

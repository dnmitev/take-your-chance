namespace ConsoleRoyale
{
    public class Player : IPlayer
    {
        private decimal _balance;

        public Player()
        {
            _balance = 0;
        }

        public decimal Balance { get => _balance; }

        public decimal GetBalance() => _balance;

        /// <inheritdoc />
        /// <exception cref="ArgumentException">When amount is below 0 which is insufficient,
        /// then a an exception is thrown to prevent faulty behaviour</exception>
        public decimal DepositToWallet(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException($"The deposited amount cannot be below 0. Given amount is {amount}");
            }

            _balance += amount;
            return _balance;
        }
    }
}

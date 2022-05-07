﻿namespace ConsoleRoyale
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

        /// <inheritdoc />
        /// <param name="amount">Positive number that represents the amount to be withdrawn from the player's wallet.</param>
        /// <exception cref="ArgumentOutOfRangeException"L>When amount is negative.</exception>
        /// <exception cref="ArgumentException">When the requested amount is greater then the current balance is player's wallet.</exception>
        public decimal Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException($"The withdrawing amont cannot be below 0. Given amount: {amount}");
            }
            else if (amount > Balance)
            {
                throw new ArgumentException($"The given amount ({amount}) is more than the wallet's balance ({Balance}) ");
            }

            _balance -= amount;

            return _balance;
        }
    }
}

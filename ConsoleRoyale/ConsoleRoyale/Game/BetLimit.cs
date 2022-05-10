namespace ConsoleRoyale
{
    public class BetLimit
    {
        public BetLimit(decimal minBet, decimal maxBet)
        {
            if (minBet > maxBet)
            {
                throw new ArgumentException("Min bet cannot be more than max bet!");
            }

            MinBet = minBet;
            MaxBet = maxBet;
        }

        public decimal MinBet { get; private set; }

        public decimal MaxBet { get; private set; }
    }
}

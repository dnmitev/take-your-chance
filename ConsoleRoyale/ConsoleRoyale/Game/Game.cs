using ConsoleRoyale.RNG;

namespace ConsoleRoyale
{
    /// <summary>
    /// A simple game that is playable
    /// </summary>
    public class Game : IPlayableGame
    {
        private const int LOW_RATE_WIN_CHANCE = 10;
        private const int LOW_RATE_MIN_MULTIPLIER = 2;
        private const int LOW_RATE_MAX_MULTIPLIER = 10;

        private const int HIGH_RATE_WIN_CHANCE = 40;
        private const int HIGH_RATE_MIN_MULTIPLIER = 1;
        private const int HIGH_RATE_MAX_MULTIPLIER = 2;

        private const int DEFAULT_MIN_BET = 1;
        private const int DEFAULT_MAX_BET = 10;

        private readonly IPlayer _player;
        private readonly IRNGProvider _rngProvider;

        private readonly BetLimit _betLimit;

        public Game(IPlayer player, IRNGProvider rngProvider)
        {
            _player = player;
            _rngProvider = rngProvider;
            _betLimit = new BetLimit(DEFAULT_MIN_BET, DEFAULT_MAX_BET);
        }

        public Game(IPlayer player, IRNGProvider rngProvider, BetLimit limit)
        {
            _player = player;
            _rngProvider = rngProvider;
            _betLimit = limit;
        }

        public IPlayer Player { get => _player; }

        /// <summary>
        /// Play a game with given bet.
        /// </summary>
        /// <param name="bet">The bettet amount</param>
        /// <returns>The winning amount or 0 if player lost the bet</returns>
        public decimal Play(decimal bet)
        {
            if (bet < _betLimit.MinBet || bet > _betLimit.MaxBet)
            {
                throw new ArgumentOutOfRangeException(
                    $"The bet should be in range [{_betLimit.MinBet}, {_betLimit.MaxBet}]");
            }

            var chance = _rngProvider.GetPercentageChance();
            decimal win = 0.0m;

            _player.Withdraw(bet);

            if (chance <= LOW_RATE_WIN_CHANCE)
            {
                win = bet * _rngProvider.GetRandomDoubleInInterval(LOW_RATE_MIN_MULTIPLIER, LOW_RATE_MAX_MULTIPLIER);
                _player.DepositToWallet(win);
            }
            else if (chance <= LOW_RATE_WIN_CHANCE + HIGH_RATE_WIN_CHANCE)
            {
                win = bet * _rngProvider.GetRandomDoubleInInterval(HIGH_RATE_MIN_MULTIPLIER, HIGH_RATE_MAX_MULTIPLIER);
                _player.DepositToWallet(win < bet ? win + bet : win);
            }

            return win;
        }
    }
}

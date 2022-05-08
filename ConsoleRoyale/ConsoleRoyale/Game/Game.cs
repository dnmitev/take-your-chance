using ConsoleRoyale.RNG;

namespace ConsoleRoyale
{
    public class Game : IGame
    {
        private readonly IPlayer _player;
        private readonly IRNGProvider _rngProvider;

        public Game(IPlayer player, IRNGProvider rngProvider)
        {
            _player = player;
            _rngProvider = rngProvider;
        }

        public decimal Play(decimal bet)
        {
            var chance = _rngProvider.GetPercentageChance();
            decimal win = 0.0m;

            _player.Withdraw(bet);

            if (chance <= 10)
            {
                win = bet * (decimal)_rngProvider.GetRandomDoubleInInterval(2, 10);
                _player.DepositToWallet(win);
            }
            else if (chance < 50)
            {
                win = bet * (decimal)_rngProvider.GetRandomDoubleInInterval(1, 2);
                _player.DepositToWallet(win);
            }

            return win;
        }
    }
}

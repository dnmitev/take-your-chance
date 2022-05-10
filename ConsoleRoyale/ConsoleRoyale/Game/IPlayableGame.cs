namespace ConsoleRoyale
{
    public interface IPlayableGame
    {
        IPlayer Player { get; }

        decimal Play(decimal bet);
    }
}

namespace OOP_ICT.Third.Exceptions.GameState;

public class GameNotStartedException : GameStateException
{
    public GameNotStartedException(string message) : base(message)
    {
    }
}
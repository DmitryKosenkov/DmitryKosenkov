namespace OOP_ICT.Third.Exceptions.GameState;

public abstract class GameStateException : Exception
{
    public GameStateException(string message) : base(message)
    {
    }
}
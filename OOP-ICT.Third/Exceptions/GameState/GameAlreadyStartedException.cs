namespace OOP_ICT.Third.Exceptions.GameState;

public class GameAlreadyStartedException : GameStateException
{
    public GameAlreadyStartedException(string message) : base(message)
    {
    }
}
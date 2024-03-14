using OOP_ICT.Third.Exceptions.GameState;

namespace OOP_ICT.Fourth.Exceptions;

public class WrongGameStateException : GameStateException
{
    public WrongGameStateException(string message) : base(message)
    {
    }
}
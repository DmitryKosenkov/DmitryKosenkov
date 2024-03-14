namespace OOP_ICT.Third.Exceptions.JoinGame;

public abstract class JoinGameException : Exception
{
    public JoinGameException(string message) : base(message)
    {
    }
}
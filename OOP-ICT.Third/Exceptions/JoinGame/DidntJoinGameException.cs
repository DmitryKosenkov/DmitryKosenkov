namespace OOP_ICT.Third.Exceptions.JoinGame;

public class DidntJoinGameException : JoinGameException
{
    public DidntJoinGameException(string message) : base(message)
    {
    }
}
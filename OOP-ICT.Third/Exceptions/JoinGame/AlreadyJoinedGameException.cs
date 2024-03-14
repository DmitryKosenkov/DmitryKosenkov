namespace OOP_ICT.Third.Exceptions.JoinGame;

public class AlreadyJoinedGameException : JoinGameException
{
    public AlreadyJoinedGameException(string message) : base(message)
    {
    }
}
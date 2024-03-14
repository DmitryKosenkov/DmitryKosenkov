namespace OOP_ICT.Models;

public class NotEnoughCardsException : Exception
{
    public NotEnoughCardsException(string message) : base(message)
    {
    }
}
namespace OOP_ICT.Second.Models.Exceptions;

public class NotEnoughFundsException : Exception
{
    public NotEnoughFundsException(string message) : base(message)
    {
    }
}

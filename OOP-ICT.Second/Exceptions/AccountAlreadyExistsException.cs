namespace OOP_ICT.Second.Models.Exceptions;

public class AccountAlreadyExistsException : Exception
{
    public AccountAlreadyExistsException(string message) : base(message)
    {
    }
}
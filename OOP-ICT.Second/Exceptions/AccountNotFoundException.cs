namespace OOP_ICT.Second.Models.Exceptions;

public class AccountNotFoundException : Exception
{
    public AccountNotFoundException(string message) : base(message)
    {
    }
}
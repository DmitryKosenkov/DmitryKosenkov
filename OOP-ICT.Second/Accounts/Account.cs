namespace OOP_ICT.Second.Models.Accounts;

public class Account
{
    public Guid PlayerId;
    public CurrencyEnum Currency;
    private uint _balance;
    
    public uint GetBalance() => _balance;
    public void SetBalance(uint balance) => _balance = balance;
}
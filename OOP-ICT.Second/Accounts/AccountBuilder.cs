namespace OOP_ICT.Second.Models.Accounts;

public class AccountBuilder
{
    private Account _account = new Account();

    public void Reset()
    {
        _account = new Account();
    }

    public AccountBuilder SetId(Guid id)
    {
        _account.PlayerId = id;
        return this;
    }

    public AccountBuilder SetCurrency(CurrencyEnum currency)
    {
        _account.Currency = currency;
        return this;
    }

    public AccountBuilder SetBalance(uint balance)
    {
        _account.SetBalance(balance);
        return this;
    }

    public Account Build()
    {
        var account = _account;
        Reset();
        return account;
    }
}
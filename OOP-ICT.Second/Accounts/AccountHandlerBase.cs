using OOP_ICT.Second.Models.Exceptions;

namespace OOP_ICT.Second.Models.Accounts;

public abstract class AccountHandlerBase
{
    protected AccountDirector AccountDirector;
    protected Dictionary<Guid, Account> Accounts = new();

    protected AccountHandlerBase(AccountDirector director)
    {
        AccountDirector = director;
    }

    public abstract void CreateAccount(Player player);

    protected Account FindAccount(Player player)
    {
        var exist = Accounts.TryGetValue(player.Id, out var account);
        if (!exist)
        {
            throw new AccountNotFoundException($"{player} account not found");
        }

        return account!;
    }

    public abstract uint Deposit(Player player, uint amount);

    public abstract uint Withdraw(Player player, uint amount);

    public abstract uint GetBalance(Player player);

    public virtual bool HasSufficientFunds(Player player, uint amount)
    {
        var balance = GetBalance(player);
        return balance >= amount;
    }
}
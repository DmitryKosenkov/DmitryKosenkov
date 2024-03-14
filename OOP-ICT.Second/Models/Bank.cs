using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Second.Models.Exceptions;

namespace OOP_ICT.Second.Models;

public class Bank : AccountHandlerBase
{
    public Bank(AccountDirector director) : base(director)
    {
    }
    
    public override void CreateAccount(Player player)
    {
        var exist = Accounts.TryGetValue(player.Id, out var account);
        if (exist)
        {
            throw new AccountAlreadyExistsException($"{player} already has account");
        }

        account = AccountDirector.BuildBankAccount(player);
        Accounts.Add(player.Id, account);
    }
    
    public override uint Deposit(Player player, uint amount)
    {
        var account = FindAccount(player);

        var balance = account.GetBalance();
        account.SetBalance(balance + amount);

        return amount;
    }
    
    public override uint Withdraw(Player player, uint amount)
    {
        var account = FindAccount(player);
        
        var balance = account.GetBalance();
        if (balance < amount)
        {
            throw new NotEnoughFundsException($"{player} account has insufficient funds for operation");
        }
        
        account.SetBalance(balance - amount);
        
        return amount;
    }

    public override uint GetBalance(Player player)
    {
        var account = FindAccount(player);

        return account.GetBalance();
    }
}
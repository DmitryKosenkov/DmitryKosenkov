using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Second.Models.Exceptions;

namespace OOP_ICT.Fourth;

public class PokerCasino : AccountHandlerBase
{
    public PokerCasino(
        AccountDirector director, 
        decimal chipsExchangeRate = 2.0M
        ) : base(director)
    {
        CasinoConfig.ChipsExchangeRate = chipsExchangeRate;
    }
    
    public override void CreateAccount(Player player)
    {
        var exist = Accounts.TryGetValue(player.Id, out var account);
        if (exist)
        {
            throw new AccountAlreadyExistsException($"{player} already has account");
        }

        account = AccountDirector.BuildCasinoAccount(player);
        Accounts.Add(player.Id, account);
    }

    public override uint Deposit(Player player, uint amount)
    {
        var account = FindAccount(player);

        var chipsAmount = ChipsHelper.MoneyToChips(amount);
        var balance = account.GetBalance();
        account.SetBalance(balance + chipsAmount);

        return chipsAmount;
    }

    public override uint Withdraw(Player player, uint amount)
    {
        var account = FindAccount(player);

        var balance = account.GetBalance();
        if (balance < amount)
        {
            throw new NotEnoughFundsException($"{player} account has insufficient funds for operation");
        }

        var moneyAmount = ChipsHelper.ChipsToMoney(amount);
        account.SetBalance(balance - moneyAmount);

        return moneyAmount;
    }

    public override uint GetBalance(Player player)
    {
        var account = FindAccount(player);

        return account.GetBalance();
    }

    public void AddChips(Player player, uint amount)
    {
        var account = FindAccount(player);

        var balance = account.GetBalance();
        account.SetBalance(balance + amount);
    }

    public void RemoveChips(Player player, uint amount)
    {
        var account = FindAccount(player);

        var balance = account.GetBalance();
        account.SetBalance(balance - amount);
    }
}
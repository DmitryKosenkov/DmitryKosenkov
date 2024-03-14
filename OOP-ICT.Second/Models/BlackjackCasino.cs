using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Second.Models.Exceptions;

namespace OOP_ICT.Second.Models;

public class BlackjackCasino : AccountHandlerBase
{
    public BlackjackCasino(
        AccountDirector director, 
        decimal winCoefficient = 1.5M, 
        decimal chipsExchangeRate = 2.0M) : base(director)
    {
        CasinoConfig.SetParameters(winCoefficient, chipsExchangeRate);
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

    public uint HandleBlackjack(Player player, uint betAmount)
    {
        var winAmount = (uint) (betAmount * CasinoConfig.WinCoefficient);
        AddChips(player: player, amount: winAmount);
        
        return winAmount;
    }
}
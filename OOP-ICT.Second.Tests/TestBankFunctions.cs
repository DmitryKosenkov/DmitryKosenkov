using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Second.Models.Exceptions;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestBankFunctions
{
    [Fact]
    public void ThrowsAccountNotFoundException_PlayerWithNoBankAccountTriesToCallMethods()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new Bank(director);

        Assert.Throws<AccountNotFoundException>(() => bank.GetBalance(player));
        Assert.Throws<AccountNotFoundException>(() => bank.Deposit(player, 100));
        Assert.Throws<AccountNotFoundException>(() => bank.Withdraw(player, 100));
        Assert.Throws<AccountNotFoundException>(() => bank.HasSufficientFunds(player, 100));
    }
    
    [Fact]
    public void ThrowsAccountAlreadyExistsException_PlayerWithBankAccountTriesToCreateAccount()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new Bank(director);
        bank.CreateAccount(player);

        Assert.Throws<AccountAlreadyExistsException>(() => bank.CreateAccount(player));
    }
    
    [Fact]
    public void AreEqual_PlayerBankAccountCreation()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new Bank(director);
        bank.CreateAccount(player);

        Assert.Equal((uint)1000, bank.GetBalance(player));
    }
    
    [Fact]
    public void ThrowsNotEnoughFundsException_PlayerTriesToOverdrawBankAccount()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new Bank(director);
        bank.CreateAccount(player);

        var balance = bank.GetBalance(player);
        Assert.Throws<NotEnoughFundsException>(() => bank.Withdraw(player, balance + 1));
    }
    
    [Fact]
    public void AreEqual_PlayerTriesToDepositAndWithdrawFromBank()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new Bank(director);
        bank.CreateAccount(player);

        var initialBalance = bank.GetBalance(player);
        var depositAmount = (uint)100;

        bank.Deposit(player, depositAmount);
        Assert.Equal(initialBalance + depositAmount, bank.GetBalance(player));

        bank.Withdraw(player, depositAmount);
        Assert.Equal(initialBalance, bank.GetBalance(player));
    }
    
    [Fact]
    public void AreEqual_PlayerHasSufficientFunds()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new Bank(director);
        bank.CreateAccount(player);

        Assert.True(bank.HasSufficientFunds(player, 0));
        Assert.False(bank.HasSufficientFunds(player, bank.GetBalance(player) + 1));
    }
}
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Second.Models.Exceptions;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestCasinoFunctions
{
    [Fact]
    public void ThrowsAccountNotFoundException_PlayerWithNoCasinoAccountTriesToCallMethods()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);

        Assert.Throws<AccountNotFoundException>(() => casino.GetBalance(player));
        Assert.Throws<AccountNotFoundException>(() => casino.Deposit(player, 100));
        Assert.Throws<AccountNotFoundException>(() => casino.Withdraw(player, 100));
        Assert.Throws<AccountNotFoundException>(() => casino.HasSufficientFunds(player, 100));
    }
    
    [Fact]
    public void ThrowsAccountAlreadyExistsException_PlayerWithCasinoAccountTriesToCreateAccount()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);
        casino.CreateAccount(player);

        Assert.Throws<AccountAlreadyExistsException>(() => casino.CreateAccount(player));
    }
    
    [Fact]
    public void AreEqual_PlayerCasinoAccountCreation()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);
        casino.CreateAccount(player);

        Assert.Equal((uint)0, casino.GetBalance(player));
    }
    
    [Fact]
    public void ThrowsNotEnoughFundsException_PlayerTriesToOverdrawCasinoAccount()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bank = new BlackjackCasino(director);
        bank.CreateAccount(player);
        
        var balance = bank.GetBalance(player);
        Assert.Throws<NotEnoughFundsException>(() => bank.Withdraw(player, balance + 1));
    }
    
    [Fact]
    public void AreEqual_PlayerTriesToDepositAndWithdrawFromCasino()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);
        casino.CreateAccount(player);

        var depositMoneyAmount = (uint)200;
        var expectedChipsAmount = (uint)(depositMoneyAmount / CasinoConfig.ChipsExchangeRate);
        
        var actualChipsAmount = casino.Deposit(player, depositMoneyAmount);
        Assert.Equal(expectedChipsAmount, actualChipsAmount);
        
        var withdrawnMoneyAmount = casino.Withdraw(player, actualChipsAmount);
        Assert.Equal(depositMoneyAmount, withdrawnMoneyAmount);
    }
    
    [Fact]
    public void AreEqual_PlayerWinsAndLosesChips()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);
        casino.CreateAccount(player);
        
        var initialBalance = casino.GetBalance(player);
        var winChipsAmount = (uint)100;
        var loseChipsAmount = (uint)50;
        
        casino.AddChips(player, winChipsAmount);
        var actualChipsAmount = casino.GetBalance(player);
        
        Assert.Equal(initialBalance + winChipsAmount, actualChipsAmount);
        
        casino.RemoveChips(player, loseChipsAmount);
        actualChipsAmount = casino.GetBalance(player);
        
        Assert.Equal(initialBalance + winChipsAmount - loseChipsAmount, actualChipsAmount);
    }
    
    [Fact]
    public void AreEqual_PlayerWinsBlackjack()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);
        casino.CreateAccount(player);
        
        var betChipsAmount = (uint)100;
        var expectedWinChipsAmount = (uint)(betChipsAmount * CasinoConfig.WinCoefficient);
        
        var actualWinChipsAmount = casino.HandleBlackjack(player, betChipsAmount);
        
        Assert.Equal(expectedWinChipsAmount, actualWinChipsAmount);
    }
    
    [Fact] 
    public void FirstIsTrueSecondIsNot_PlayerHasSufficientFundsForOnly1Case()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var casino = new BlackjackCasino(director);
        casino.CreateAccount(player);
        
        var sufficientChipsAmount = (uint)100;
        var insufficientChipsAmount = (uint)200;
        
        casino.AddChips(player, sufficientChipsAmount);
        Assert.True(casino.HasSufficientFunds(player, sufficientChipsAmount));
        Assert.False(casino.HasSufficientFunds(player, insufficientChipsAmount));
    }
}
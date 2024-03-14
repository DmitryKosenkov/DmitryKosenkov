using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestAccountBuilderFunctions
{

    [Fact]
    public void AreEqual_FreshBankAccountCreation()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bankAccount = director.BuildBankAccount(player);

        Assert.Equal(CurrencyEnum.Money, bankAccount.Currency);
        Assert.Equal((uint)1000, bankAccount.GetBalance());
    }
    
    [Fact]
    public void AreEqual_FreshCasinoAccountCreation()
    {
        var director = new AccountDirector();
        var player = new Player("Alex");
        var bankAccount = director.BuildCasinoAccount(player);

        Assert.Equal(CurrencyEnum.Chips, bankAccount.Currency);
        Assert.Equal((uint)0, bankAccount.GetBalance());
    }
}
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Second.Tests;

public class TestPlayerFunctions
{
    [Fact]
    public void AreEqual_PlayerTakesAndGivesCards()
    {
        var player = new Player("Alex");
        var dealer = new Dealer();
        
        var cards1 = dealer.DealCards(5);
        player.TakeCards(cards1);
        
        Assert.Equal(5, player.Cards.Count);
        Assert.Equal(cards1, player.Cards);

        var cards2 = dealer.DealCards(10);
        player.TakeCards(cards2);
        
        Assert.Equal(15, player.Cards.Count);
        Assert.Equal(cards1.Concat(cards2), player.Cards);
        
        var cards3 = player.GiveCards();
        Assert.Empty(player.Cards);
        Assert.Equal(cards1.Concat(cards2), cards3);
    }
}
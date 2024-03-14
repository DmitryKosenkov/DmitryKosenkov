using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    [Fact]
    public void AreEqual_InputIsSameCards_ReturnTrue()
    {
        var suit = CardSuit.Clubs;
        var value = CardRank.Ace;
        var card1 = new Card(suit, value);
        var card2 = new Card(suit, value);
        
        Assert.Equal(card1, card2);
    }
    
    [Fact]
    public void AreNotEqual_InputIsDifferentCards_ReturnTrue()
    {
        var suit1 = CardSuit.Clubs;
        var value1 = CardRank.Ace;
        var card1 = new Card(suit1, value1);
        
        var suit2 = CardSuit.Diamonds;
        var value2 = CardRank.Two;
        var card2 = new Card(suit2, value2);
        
        Assert.NotEqual(card1, card2);
    }
}
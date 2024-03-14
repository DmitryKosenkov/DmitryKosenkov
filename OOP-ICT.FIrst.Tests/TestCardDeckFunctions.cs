using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardDeckFunctions
{
    [Fact]
    public void AreEqual_FirstAndLastCardFromNewDeck_ReturnTrue()
    {
        var deck = new CardDeck();
        var firstCard = deck.Cards.First();
        var lastCard = deck.Cards.Last();

        Assert.Equal(CardRank.Ace, firstCard.Rank);
        Assert.Equal(CardRank.Two, lastCard.Rank);
        
        Assert.Equal(52, deck.Cards.Count);
    }
}
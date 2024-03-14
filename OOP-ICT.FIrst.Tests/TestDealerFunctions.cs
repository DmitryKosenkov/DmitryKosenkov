using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestDealerFunctions
{
    [Fact]
    public void AreEqual_NotShuffledDeckAndDealersNewDeck_ReturnTrue()
    {
        var dealer = new Dealer();
        var notShuffledDeck = new CardDeck();

        Assert.Equal(notShuffledDeck.Cards, dealer.Cards);
    }
    
    [Fact]
    public void AreNotEqual_NotShuffledDeckAndShuffledDeck_ReturnTrue()
    {
        var notShuffledDeck = new CardDeck();

        var dealer = new Dealer();
        dealer.ShuffleDeck();

        Assert.NotEqual(notShuffledDeck.Cards, dealer.Cards);
    }
    
    [Fact]
    public void ThrowsInvalidOperationException_DealerWithDeckWithNoCardsLeft_ReturnTrue()
    {
        var dealer = new Dealer();
        Assert.Throws<NotEnoughCardsException>(() => dealer.DealCards(dealer.Cards.Count + 1));
    }
    
    [Fact]
    public void AreEqual_DealerGivingAndTakingCardsBack_ReturnTrue()
    {
        var dealer = new Dealer();

        var cards = dealer.DealCards(5);
        Assert.Equal(5, cards.Count);
        Assert.Equal(47, dealer.Cards.Count);

        dealer.TakeCards(cards);
        Assert.Equal(52, dealer.Cards.Count);
    }
}
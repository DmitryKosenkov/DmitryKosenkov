using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestScoreHelper
{
    
    [Fact]
    public void AreEqual_EmptyPlayerHand()
    {
        var player = new Player("Alex");
        
        Assert.Equal(0, ScoreHelper.CalculateHandScore(player.Cards));
    }
    
    [Fact]
    public void AreEqual_PlayerHand2Aces()
    {
        var player = new Player("Alex");
        
        var cards = new[] {
            new Card(CardSuit.Clubs, CardRank.Ace),
            new Card(CardSuit.Clubs, CardRank.Ace)
        };
        player.TakeCards(cards);

        var expectedScore = BlackjackGameConfig.AceValueOverBlackJack + BlackjackGameConfig.AceValueUnderBlackJack;
        
        Assert.Equal(expectedScore, ScoreHelper.CalculateHandScore(player.Cards));
    }
    
    [Fact]
    public void AreEqual_PlayerHand1Ace1Picture()
    {
        var player = new Player("Alex");
        
        // 1 туз и 1 "картинка": 21 очко = 11 + 10
        var cards = new[] {
            new Card(CardSuit.Clubs, CardRank.Ace),
            new Card(CardSuit.Clubs, CardRank.Queen)
        };
        player.TakeCards(cards);
        
        var expectedScore = BlackjackGameConfig.AceValueOverBlackJack + BlackjackGameConfig.PictureCardValue;

        Assert.Equal(expectedScore, ScoreHelper.CalculateHandScore(player.Cards));
    }
    
    [Fact]
    public void AreEqual_PlayerHandSimpleCards()
    {
        var player = new Player("Alex");
        
        // Двойка, Тройка и Четверка: 9 очков = 2 + 3 + 4
        var cards = new[] {
            new Card(CardSuit.Clubs, CardRank.Two),
            new Card(CardSuit.Clubs, CardRank.Three),
            new Card(CardSuit.Clubs, CardRank.Four)
        };
        player.TakeCards(cards);
        
        var expectedScore = cards.Sum(card => (int)card.Rank);
        
        Assert.Equal(expectedScore, ScoreHelper.CalculateHandScore(player.Cards));
    }
}
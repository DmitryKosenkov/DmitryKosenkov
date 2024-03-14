using OOP_ICT.Fourth.PokerCombinations;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class TestPokerCombinationHelper
{
    
    // Проверка распознания комбинаций карт
    [Fact]
    public void AreEqual_AllCombinationsRecognition()
    {
        var cardsRoyalFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Clubs, CardRank.Ten)
        };
        var cardsStraightFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five)
        };
        var cardsFourOfAKind = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Hearts, CardRank.Ace),
            new(CardSuit.Spades, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.Ten)
        };
        var cardsFullHouse = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Hearts, CardRank.Ace),
            new(CardSuit.Spades, CardRank.Ten),
            new(CardSuit.Clubs, CardRank.Ten)
        };
        var cardsFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ten),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Five),
            new(CardSuit.Clubs, CardRank.King)
        };
        var cardsStraight = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Diamonds, CardRank.Eight),
            new(CardSuit.Hearts, CardRank.Seven),
            new(CardSuit.Spades, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five)
        };
        var cardsThreeOfAKind = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Hearts, CardRank.Ace),
            new(CardSuit.Spades, CardRank.Ten),
            new(CardSuit.Clubs, CardRank.Two)
        };
        var cardsTwoPair = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Hearts, CardRank.Ten),
            new(CardSuit.Spades, CardRank.Ten),
            new(CardSuit.Clubs, CardRank.Two)
        };
        var cardsOnePair = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Hearts, CardRank.Ten),
            new(CardSuit.Spades, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Two)
        };
        var cardsHighCard = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.King),
            new(CardSuit.Hearts, CardRank.Ten),
            new(CardSuit.Spades, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Two)
        };
        Assert.Equal(PokerCombinationEnum.RoyalFlush, PokerCombinationHelper.DetermineCombination(cardsRoyalFlush));
        Assert.Equal(PokerCombinationEnum.StraightFlush, PokerCombinationHelper.DetermineCombination(cardsStraightFlush));
        Assert.Equal(PokerCombinationEnum.FourOfAKind, PokerCombinationHelper.DetermineCombination(cardsFourOfAKind));
        Assert.Equal(PokerCombinationEnum.FullHouse, PokerCombinationHelper.DetermineCombination(cardsFullHouse));
        Assert.Equal(PokerCombinationEnum.Flush, PokerCombinationHelper.DetermineCombination(cardsFlush));
        Assert.Equal(PokerCombinationEnum.Straight, PokerCombinationHelper.DetermineCombination(cardsStraight));
        Assert.Equal(PokerCombinationEnum.ThreeOfAKind, PokerCombinationHelper.DetermineCombination(cardsThreeOfAKind));
        Assert.Equal(PokerCombinationEnum.TwoPair, PokerCombinationHelper.DetermineCombination(cardsTwoPair));
        Assert.Equal(PokerCombinationEnum.OnePair, PokerCombinationHelper.DetermineCombination(cardsOnePair));
        Assert.Equal(PokerCombinationEnum.HighCard, PokerCombinationHelper.DetermineCombination(cardsHighCard));
    }
    
    // Проверка выявления победителя среди игроков с разными комбинациями
    [Fact]
    public void AreEqual_TwoPlayersWithNotMatchingCardCombinations()
    {
        var cardsRoyalFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Clubs, CardRank.Ten)
        };
        var cardsStraightFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Nine),
            new(CardSuit.Clubs, CardRank.Eight),
            new(CardSuit.Clubs, CardRank.Seven),
            new(CardSuit.Clubs, CardRank.Six),
            new(CardSuit.Clubs, CardRank.Five)
        };
        
        var player1 = new Player("Alex");
        var player2 = new Player("Bob");
        
        player1.TakeCards(cardsRoyalFlush);
        player2.TakeCards(cardsStraightFlush);
        
        var players = new List<Player> {player1, player2};
        var tableCards = new List<Card>();
        var winners = PokerCombinationHelper.DetermineWinners(players, tableCards);
        
        Assert.Single(winners);
        Assert.Equal(player1, winners[0]);
    }
    
    // Проверка ничьей между игроками
    [Fact]
    public void AreEqual_TwoPlayersWithMatchingCardCombinations()
    {
        var cardsRoyalFlush = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Clubs, CardRank.Ten)
        };
        var cardsRoyalFlush2 = new List<Card>
        {
            new(CardSuit.Diamonds, CardRank.Ace),
            new(CardSuit.Diamonds, CardRank.King),
            new(CardSuit.Diamonds, CardRank.Queen),
            new(CardSuit.Diamonds, CardRank.Jack),
            new(CardSuit.Diamonds, CardRank.Ten)
        };
        
        var player1 = new Player("Alex");
        var player2 = new Player("Bob");
        
        player1.TakeCards(cardsRoyalFlush);
        player2.TakeCards(cardsRoyalFlush2);
        
        var players = new List<Player> {player1, player2};
        var tableCards = new List<Card>();
        var winners = PokerCombinationHelper.DetermineWinners(players, tableCards);
        
        Assert.Equal(2, winners.Count);
        Assert.Contains(player1, winners);
        Assert.Contains(player2, winners);
    }

}
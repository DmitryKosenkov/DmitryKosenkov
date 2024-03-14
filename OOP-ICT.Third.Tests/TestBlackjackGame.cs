using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Third.Exceptions;
using OOP_ICT.Third.Exceptions.GameState;
using OOP_ICT.Third.Exceptions.JoinGame;
using Xunit;

namespace OOP_ICT.Third.Tests;

public class TestBlackjackGame
{
    [Fact]
    public void ThrowsAlreadyJoinedGameException_SecondGameJoin()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer);

        casino.CreateAccount(player);
        casino.AddChips(player, 1000);
        game.Join(player, 1);

        Assert.Throws<AlreadyJoinedGameException>(() => game.Join(player, 1));
    }
    
    [Fact]
    public void ThrowsBetTooSmall_PlayerBetsSmallerThanMinimalBet()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer, minimalBet: 10);

        casino.CreateAccount(player);
        casino.AddChips(player, 1000);

        Assert.Throws<BetTooSmallException>(() => game.Join(player, game.MinimalBet - 1));
    }
    
    [Fact]
    public void ThrowsDidntJoinGame_PlayerHitsWhenDidntJoinGame()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer, minimalBet: 10);
        
        game.Start();

        Assert.Throws<DidntJoinGameException>(() => game.Hit(player));
    }
    
    [Fact]
    public void ThrowsGameNotStarted_PlayerHitsAndGameTriesToFinishWhenGameNotStarted()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer, minimalBet: 10);
        
        Assert.Throws<GameNotStartedException>(() => game.Finish());
        Assert.Throws<GameNotStartedException>(() => game.Hit(player));
    }
    
    [Fact]
    public void ThrowsGameAlreadyStarted_PlayerJoinsAndGameTriesToStartWhenGameStarted()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer, minimalBet: 10);
        
        casino.CreateAccount(player);
        casino.AddChips(player, 1000);
        
        game.Start();
        
        Assert.Throws<GameAlreadyStartedException>(() => game.Start());
        Assert.Throws<GameAlreadyStartedException>(() => game.Join(player, 1));
    }
    
    [Fact] public void AreEqual_PlayerWinsBlackjackOnFirst2Cards()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer);
        
        var playerStartBalance = (uint)1000;
        var playerBet = (uint)10;
        
        casino.CreateAccount(player);
        casino.AddChips(player, playerStartBalance);
        game.Join(player, playerBet);
        game.Start();

        var cards = new[] {
            new Card(CardSuit.Clubs, CardRank.Ace),
            new Card(CardSuit.Clubs, CardRank.Queen)
        };
        
        player.GiveCards();
        player.TakeCards(cards);
        game.Finish();

        var playerBalance = casino.GetBalance(player);
        var expectedBalance = playerStartBalance + playerBet * CasinoConfig.WinCoefficient;
        
        Assert.Equal(expectedBalance, playerBalance);
    }
    
    [Fact] public void AreEqual_PlayerWinsStandartWay()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer);
        
        var playerStartBalance = (uint)1000;
        var playerBet = (uint)10;
        
        casino.CreateAccount(player);
        casino.AddChips(player, playerStartBalance);
        game.Join(player, playerBet);
        game.Start();

        var cards = new[] {
            new Card(CardSuit.Clubs, CardRank.King),
            new Card(CardSuit.Clubs, CardRank.Queen)
        };
        
        player.GiveCards();
        player.TakeCards(cards);
        game.Finish();

        var playerBalance = casino.GetBalance(player);
        var expectedBalance = playerStartBalance + playerBet;
        
        Assert.Equal(expectedBalance, playerBalance);
    }
    
    [Fact] public void AreEqual_PlayerLosesBet()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer);
        
        var playerStartBalance = (uint)1000;
        var playerBet = (uint)10;
        
        dealer.ShuffleDeck();
        casino.CreateAccount(player);
        casino.AddChips(player, playerStartBalance);
        game.Join(player, playerBet);
        game.Start();

        var cards = new[] {
            new Card(CardSuit.Clubs, CardRank.Two),
            new Card(CardSuit.Spades, CardRank.Two)
        };
        
        player.GiveCards();
        player.TakeCards(cards);
        game.Finish();

        var playerBalance = casino.GetBalance(player);
        var expectedBalance = playerStartBalance - playerBet;
        
        Assert.Equal(expectedBalance, playerBalance);
    }
    
    [Fact] public void IsEmpty_PlayersReturnCardsAfterGameFinishes()
    {
        var player = new Player("Alex");

        var dealer = new Dealer();
        var director = new AccountDirector();
        var casino = new BlackjackCasino(director);
        var game = new BlackjackGame(casino: casino, dealer: dealer);
        
        var playerStartBalance = (uint)1000;
        var playerBet = (uint)10;
        
        dealer.ShuffleDeck();
        casino.CreateAccount(player);
        casino.AddChips(player, playerStartBalance);
        game.Join(player, playerBet);
        game.Start();
        game.Finish();

        Assert.Empty(player.Cards);
        Assert.Equal(52, dealer.Cards.Count);
    }
    
}
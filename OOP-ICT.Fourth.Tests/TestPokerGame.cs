using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Second.Models.Exceptions;
using OOP_ICT.Third.Exceptions.JoinGame;
using Xunit;

namespace OOP_ICT.Fourth.Tests;

public class TestPokerGame
{
    // Проверка начисления выигрыша и снятия проигрыша в конце игры
    [Fact]
    public void AreEqual_OnePlayerWinsBetAndOtherLosesBet()
    {
        var player1 = new Player("Alex");
        var player2 = new Player("Steven");
        var dealer = new Player("Bob");
        
        var director = new AccountDirector();
        var casino = new PokerCasino(director);
        casino.CreateAccount(player1);
        casino.CreateAccount(player2);
        
        var playersInitialBalance = (uint) 1000;
        casino.AddChips(player1, playersInitialBalance);
        casino.AddChips(player2, playersInitialBalance);
        
        var game = new PokerGame(casino: casino, dealer: dealer);
        game.Join(player1);
        game.Join(player2);
        game.NextTurn();
        
        var playersBet = (uint) 100;
        game.PlaceBet(player1, playersBet);
        game.PlaceBet(player2, playersBet);
        
        var cardsWin = new List<Card>
        {
            new(CardSuit.Clubs, CardRank.Ace),
            new(CardSuit.Clubs, CardRank.King),
            new(CardSuit.Clubs, CardRank.Queen),
            new(CardSuit.Clubs, CardRank.Jack),
            new(CardSuit.Clubs, CardRank.Ten)
        };

        player1.GiveCards();
        player1.TakeCards(cardsWin);
        player2.GiveCards();
        
        game.Finish();
        
        Assert.Equal(playersInitialBalance + PokerGameConfig.BetToJoin + playersBet, casino.GetBalance(player1));
        Assert.Equal(playersInitialBalance - PokerGameConfig.BetToJoin - playersBet, casino.GetBalance(player2));
    }
    
    // Проверка запретов дилеру на попытки участия в игре
    [Fact]
    public void ThrowsInvalidRoleOperationException_DealerTriesToPlay()
    {
        var player = new Player("Alex");
        var dealer = new Player("Bob");
        
        var director = new AccountDirector();
        var casino = new PokerCasino(director);
        casino.CreateAccount(player);
        casino.CreateAccount(dealer);
        
        var playersInitialBalance = (uint) 1000;
        casino.AddChips(player, playersInitialBalance);
        
        var game = new PokerGame(casino: casino, dealer: dealer);
        
        Assert.Throws<InvalidRoleOperationException>(() => game.Join(dealer));
        game.Join(player);
        
        game.NextTurn();
        Assert.Throws<InvalidRoleOperationException>(() => game.PlaceBet(dealer, 10));
    }
    
    // Проверка запретов на участие в игре при недостатке фишек на счету казино
    [Fact]
    public void ThrowsNotEnoughFundsException_PlayerTriesToPlayWithNoFunds()
    {
        var player = new Player("Alex");
        var dealer = new Player("Bob");
        
        var director = new AccountDirector();
        var casino = new PokerCasino(director);
        casino.CreateAccount(player);
        casino.CreateAccount(dealer);
        
        var game = new PokerGame(casino: casino, dealer: dealer);
        
        Assert.Throws<NotEnoughFundsException>(() => game.Join(player));
        
        casino.AddChips(player, 100);
        game.Join(player);
        
        game.NextTurn();
        Assert.Throws<NotEnoughFundsException>(() => game.PlaceBet(player, 1000));
    }
    
    // Проверка запретов на участие в игре, если не присоединился к игре с самого начала
    [Fact]
    public void ThrowsDidntJoinGameException_PlayerTriesToPlayWithoutJoining()
    {
        var player = new Player("Alex");
        var dealer = new Player("Bob");
        
        var director = new AccountDirector();
        var casino = new PokerCasino(director);
        casino.CreateAccount(player);
        casino.CreateAccount(dealer);
        casino.AddChips(player, 1000000);
        
        var game = new PokerGame(casino: casino, dealer: dealer);
        
        game.NextTurn();
        Assert.Throws<DidntJoinGameException>(() => game.PlaceBet(player, 1000));
        game.NextTurn();
        Assert.Throws<DidntJoinGameException>(() => game.PlaceBet(player, 1000));
    }
    
    // Проверка обязательной смены дилера после игры
    [Fact]
    public void AreEqual_DealerChangesAfterGame()
    {
        var player = new Player("Alex");
        var dealer1 = new Player("Bob");
        var dealer2 = new Player("Steven");
        
        var director = new AccountDirector();
        var casino = new PokerCasino(director);
        casino.CreateAccount(player);
        casino.CreateAccount(dealer1);
        casino.AddChips(player, 1000000);
        
        var game = new PokerGame(casino: casino, dealer: dealer1);
        
        game.Join(player);
        game.NextTurn();
        game.Finish();

        Assert.Throws<WrongGameStateException>(() => game.Join(player));
        Assert.Throws<AlreadyWasDealerException>(() => game.ChangeDealer(dealer1));
        game.ChangeDealer(dealer2);
        game.Join(player);
    }
}
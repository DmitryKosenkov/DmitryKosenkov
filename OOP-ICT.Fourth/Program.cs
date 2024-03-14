using OOP_ICT.Fourth;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;

var player1 = new Player("Alex");
var player2 = new Player("Steven");
var player3 = new Player("Bob");
        
var director = new AccountDirector();
var casino = new PokerCasino(director);
casino.CreateAccount(player1);
casino.CreateAccount(player2);
        
var playersInitialBalance = (uint) 1000;
casino.AddChips(player1, playersInitialBalance);
casino.AddChips(player2, playersInitialBalance);
        
var game = new PokerGame(casino: casino, dealer: player3);
game.Join(player1);
game.Join(player2);
game.NextTurn();
        
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
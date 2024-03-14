using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;
using OOP_ICT.Third;

var player = new Player("Alex");

var dealer = new Dealer();
var director = new AccountDirector();
var casino = new BlackjackCasino(director);
var game = new BlackjackGame(casino: casino, dealer: dealer);
        
dealer.ShuffleDeck();
casino.CreateAccount(player);
casino.AddChips(player, 1000);
game.Join(player, 10);
game.Start();

var cards = new[] {
    new Card(CardSuit.Clubs, CardRank.Ace),
    new Card(CardSuit.Spades, CardRank.Queen)
};
        
player.GiveCards();
player.TakeCards(cards);

game.Finish();

var balance = casino.GetBalance(player);

Console.WriteLine(balance);
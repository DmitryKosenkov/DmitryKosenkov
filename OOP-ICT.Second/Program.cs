using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Accounts;

var player = new Player("Alex");
var director = new AccountDirector();
var bank = new Bank(director);
var casino = new BlackjackCasino(director, 2.5M, 3M);

bank.CreateAccount(player);
casino.CreateAccount(player);

Console.WriteLine("Initial bank balance: " + bank.GetBalance(player));
Console.WriteLine("Initial casino balance: " + casino.GetBalance(player));

var money = bank.Withdraw(player, (uint)100);
var chips = casino.Deposit(player, money);

Console.WriteLine($"Took {money} money from bank");
Console.WriteLine($"Gave {money} money to casino and recieved {chips} chips");

var wonChips = casino.HandleBlackjack(player, chips);
Console.WriteLine($"Got blackjack and won {wonChips} chips");
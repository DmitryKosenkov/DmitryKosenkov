using OOP_ICT.Models;

var dealer = new Dealer();
dealer.ShuffleDeck();
Console.WriteLine($"Cards in deck: {dealer.Cards.Count}");

var cards = dealer.DealCards(5);
Console.WriteLine($"Cards in deck: {dealer.Cards.Count}");
Console.WriteLine(string.Join("\n", cards));

dealer.TakeCards(cards);
Console.WriteLine($"Cards in deck: {dealer.Cards.Count}");

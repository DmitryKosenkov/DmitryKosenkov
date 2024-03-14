namespace OOP_ICT.Models;

public class CardDeck
{
    public readonly List<Card> Cards;
    
    public CardDeck()
    {
        var suits = Enum.GetValues(typeof(CardSuit)).Cast<CardSuit>();
        var ranks = Enum.GetValues(typeof(CardRank)).Cast<CardRank>();

        Cards = ranks.Reverse()
            .SelectMany(rank => suits.Select(suit => new Card(suit, rank))).ToList();
    }
    
    public override bool Equals(object obj)
    {
        var deck = obj as CardDeck;
        if (deck is null)
        {
            return false;
        }

        return Cards.SequenceEqual(deck.Cards);
    }
}
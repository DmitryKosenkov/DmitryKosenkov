namespace OOP_ICT.Models;

public class Dealer
{
    private readonly CardDeck _deck = new();
    private readonly IPerfectShuffle _shuffler;
    
    public Dealer(IPerfectShuffle shuffler = null)
    {
        _shuffler = shuffler ?? new PerfectShuffle();
    }

    public IReadOnlyCollection<Card> Cards => _deck.Cards;

    public void ShuffleDeck()
    {
        _shuffler.Shuffle(_deck.Cards);
    }
    
    public List<Card> DealCards(int amount)
    {
        if (_deck.Cards.Count < amount)
        {
            throw new NotEnoughCardsException($"There's less than {amount} cards in the deck.");
        }
        
        var cards = _deck.Cards.Take(amount).ToList();
        _deck.Cards.RemoveRange(0, amount);
        
        return cards;
    }

    public void TakeCards(IEnumerable<Card> cards)
    {
        _deck.Cards.AddRange(cards);
    }
}
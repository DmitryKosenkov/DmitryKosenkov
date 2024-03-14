using System.Collections.ObjectModel;
using OOP_ICT.Models;

namespace OOP_ICT.Second.Models;

public class Player
{
    public Guid Id { get; }
    public string Name { get; private set; }
    private List<Card> _cards = new();
    
    public Player(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public ReadOnlyCollection<Card> Cards => _cards.AsReadOnly();

    public void TakeCards(IEnumerable<Card> cards)
    {
        _cards.AddRange(cards);
    }
    
    public List<Card> GiveCards()
    {
        var cards = _cards;
        _cards = new List<Card>();
        return cards;
    }

    public override string ToString()
    {
        return $"Player (id: {Id})";
    }
}
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth;

public class PlayerDealer
{
    private readonly Dealer _dealer;
    public readonly Player Player;

    public PlayerDealer(Player player, IPerfectShuffle shuffler = null)
    {
        Player = player;
        _dealer = new Dealer(shuffler);
    }

    public void TakeCards(IEnumerable<Card> cards) => _dealer.TakeCards(cards);
    
    public List<Card> DealCards(int amount = 0) => _dealer.DealCards(amount);
    
    public void ShuffleDeck() => _dealer.ShuffleDeck();
    
    public IReadOnlyCollection<Card> Cards => _dealer.Cards;

    public override string ToString() => Player.ToString();
}
namespace OOP_ICT.Models;

public class Card
{
    public readonly CardSuit Suit;
    public readonly CardRank Rank;

    public Card(CardSuit suit, CardRank rank)
    {
        Rank = rank;
        Suit = suit;
    }

    public override bool Equals(object obj)
    {
        var card = obj as Card;
        if (card is null)
        {
            return false;
        }

        return this.Suit == card.Suit && this.Rank == card.Rank;
    }

    public override string ToString()
    {
        return $"Card: {this.Rank} of {this.Suit}";
    }
}
namespace OOP_ICT.Models;

public class PerfectShuffle : IPerfectShuffle
{
    public void Shuffle(List<Card> cards)
    {
        var halfLength = cards.Count / 2;
        var shuffledCards = new List<Card>();

        for (var i = 0; i < halfLength; i++)
        {
            shuffledCards.Add(cards[i + halfLength]);
            shuffledCards.Add(cards[i]);
        }

        cards.Clear();
        cards.AddRange(shuffledCards);
    }
}
using OOP_ICT.Models;

namespace OOP_ICT.Third;

public static class ScoreHelper
{
    public static int CalculateHandScore(IEnumerable<Card> hand)
    {
        var score = 0;
        var numberOfAces = 0;

        foreach (var cardValue in hand.Select(GetCardValue))
        {
            score += cardValue;

            if (cardValue == BlackjackGameConfig.AceValueOverBlackJack)
            {
                numberOfAces++;
            }

            while (score > BlackjackGameConfig.BlackJackScore && numberOfAces > 0)
            {
                score -= (BlackjackGameConfig.AceValueOverBlackJack - BlackjackGameConfig.AceValueUnderBlackJack);
                numberOfAces--;
            }
        }

        return score;
    }

    public static int GetCardValue(Card card)
    {
        if (card.Rank == CardRank.Ace)
        {
            return BlackjackGameConfig.AceValueOverBlackJack;
        }

        if (new []{CardRank.King, CardRank.Queen, CardRank.Jack}.Contains(card.Rank))
        {
            return BlackjackGameConfig.PictureCardValue;
        }

        return (int)card.Rank;
    }
}
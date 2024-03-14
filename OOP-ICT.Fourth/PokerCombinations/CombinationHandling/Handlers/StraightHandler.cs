using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class StraightHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsStraight(cards)) return PokerCombinationEnum.Straight;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsStraight(List<Card> cards)
    {
        for (int i = 0; i < cards.Count - 4; i++)
        {
            if (cards[i].Rank - cards[i + 4].Rank == 4)
            {
                return true;
            }
        }
        return false;
    }
}
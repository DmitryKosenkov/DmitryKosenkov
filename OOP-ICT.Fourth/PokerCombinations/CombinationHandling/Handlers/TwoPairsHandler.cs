using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class TwoPairsHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsTwoPairs(cards)) return PokerCombinationEnum.TwoPair;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsTwoPairs(List<Card> cards)
    {
        var groups = cards.GroupBy(c => c.Rank);
        return groups.Count(g => g.Count() == 2) == 2;
    }
}
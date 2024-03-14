using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class ThreeOfAKindHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsThreeOfAKind(cards)) return PokerCombinationEnum.ThreeOfAKind;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsThreeOfAKind(List<Card> cards)
    {
        return cards.GroupBy(c => c.Rank).Any(g => g.Count() == 3);
    }
}
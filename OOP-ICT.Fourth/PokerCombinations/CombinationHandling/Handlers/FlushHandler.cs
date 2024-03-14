using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class FlushHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsFlush(cards)) return PokerCombinationEnum.Flush;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsFlush(List<Card> cards)
    {
        return cards.GroupBy(c => c.Suit).Any(g => g.Count() >= 5);
    }
}
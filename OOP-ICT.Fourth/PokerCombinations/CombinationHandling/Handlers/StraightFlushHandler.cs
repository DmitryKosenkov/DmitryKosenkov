using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class StraightFlushHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsStraightFlush(cards)) return PokerCombinationEnum.StraightFlush;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsStraightFlush(List<Card> cards)
    {
        return StraightHandler.IsStraight(cards) && FlushHandler.IsFlush(cards);
    }
}
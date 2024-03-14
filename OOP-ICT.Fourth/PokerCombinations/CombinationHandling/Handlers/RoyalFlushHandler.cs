using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class RoyalFlushHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsRoyalFlush(cards)) return PokerCombinationEnum.RoyalFlush;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsRoyalFlush(List<Card> cards)
    {
        return StraightFlushHandler.IsStraightFlush(cards) && cards.Any(c => c.Rank == CardRank.Ace);
    }
}
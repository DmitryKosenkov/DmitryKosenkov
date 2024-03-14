using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;

public class FullHouseHandler : IPokerCombinationHandler
{
    private IPokerCombinationHandler _nextHandler;

    public PokerCombinationEnum Handle(List<Card> cards)
    {
        if (IsFullHouse(cards)) return PokerCombinationEnum.FullHouse;
        return _nextHandler?.Handle(cards) ?? PokerCombinationEnum.HighCard;
    }

    public IPokerCombinationHandler SetNext(IPokerCombinationHandler handler)
    {
        _nextHandler = handler;
        return handler;
    }

    public static bool IsFullHouse(List<Card> cards)
    {
        return ThreeOfAKindHandler.IsThreeOfAKind(cards) && OnePairHandler.IsOnePair(cards);
    }
}
using OOP_ICT.Fourth.PokerCombinations.CombinationHandling.Handlers;
using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling;

public class PokerCombinationHandlerChain
{
    private IPokerCombinationHandler _handlerChain;

    public PokerCombinationHandlerChain()
    {
        _handlerChain = new RoyalFlushHandler();
        _handlerChain
            .SetNext(new StraightFlushHandler())
            .SetNext(new FourOfAKindHandler())
            .SetNext(new FullHouseHandler())
            .SetNext(new FlushHandler())
            .SetNext(new StraightHandler())
            .SetNext(new ThreeOfAKindHandler())
            .SetNext(new TwoPairsHandler())
            .SetNext(new OnePairHandler());
    }

    public PokerCombinationEnum Handle(List<Card> cards) => _handlerChain.Handle(cards);
    
}

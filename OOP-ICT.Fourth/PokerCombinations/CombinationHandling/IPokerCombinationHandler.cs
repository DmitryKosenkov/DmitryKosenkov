using OOP_ICT.Models;

namespace OOP_ICT.Fourth.PokerCombinations.CombinationHandling;

public interface IPokerCombinationHandler
{
    PokerCombinationEnum Handle(List<Card> cards);
    IPokerCombinationHandler SetNext(IPokerCombinationHandler handler);
}
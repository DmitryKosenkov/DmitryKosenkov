using OOP_ICT.Fourth.PokerCombinations.CombinationHandling;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;

namespace OOP_ICT.Fourth.PokerCombinations;

public static class PokerCombinationHelper
{

    public static List<Player> DetermineWinners(List<Player> players, List<Card> tableCards)
    {
        var highestCombination = PokerCombinationEnum.HighCard;
        var winners = new List<Player>();
        
        foreach (var player in players)
        {
            var playerCombination = DetermineCombination(player.Cards.Concat(tableCards).ToList());
            if (playerCombination > highestCombination)
            {
                highestCombination = playerCombination;
                winners.Clear();
                winners.Add(player);
            }
            else if (playerCombination == highestCombination && winners.Count != 0)
            {
                if (winners.Count != 0)
                {
                    var winnerExample = winners[0];
                    var comparison = DetermineWinnerInPair(winnerExample, player, tableCards);
                    switch (comparison)
                    {
                        case PokerCombinationComparisonEnum.FirstWins:
                            break;
                        case PokerCombinationComparisonEnum.SecondWins:
                            winners.Clear();
                            winners.Add(player);
                            break;
                        case PokerCombinationComparisonEnum.Equal:
                            winners.Add(player);
                            break;
                    }
                }
                else
                {
                    winners.Add(player);

                }
            }
        }

        return winners;
    }
    
    private static PokerCombinationComparisonEnum DetermineWinnerInPair(Player player1, Player player2, List<Card> tableCards)
    {
        
        var player1Table = player1.Cards.Concat(tableCards).ToList();
        var player2Table = player2.Cards.Concat(tableCards).ToList();
        
        var player1Combination = DetermineCombination(player1Table);
        var player2Combination = DetermineCombination(player2Table);

        if (player1Combination > player2Combination)
        {
            return PokerCombinationComparisonEnum.FirstWins;
        }
        else if (player1Combination < player2Combination)
        {
            return PokerCombinationComparisonEnum.SecondWins;
        }
        else
        {
            return DetermineWinnerByHighCard(player1, player2, tableCards);
        }
    }

    private static PokerCombinationComparisonEnum DetermineWinnerByHighCard(Player player1, Player player2, List<Card> tableCards)
    {
        var player1Table = player1.Cards.Concat(tableCards).ToList();
        var player2Table = player2.Cards.Concat(tableCards).ToList();
        
        var player1HighCard = player1Table.Max(c => c.Rank);
        var player2HighCard = player2Table.Max(c => c.Rank);

        if (player1HighCard > player2HighCard)
        {
            return PokerCombinationComparisonEnum.FirstWins;
        }
        else if (player1HighCard < player2HighCard)
        {
            return PokerCombinationComparisonEnum.SecondWins;
        }
        {
            return PokerCombinationComparisonEnum.Equal;
        }
    }

    public static PokerCombinationEnum DetermineCombination(List<Card> cards)
    {
        var handlerChain = new PokerCombinationHandlerChain();
        return handlerChain.Handle(cards);
    }
}

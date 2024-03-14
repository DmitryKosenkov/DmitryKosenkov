using System.Collections.ObjectModel;
using OOP_ICT.Fourth.Exceptions;
using OOP_ICT.Fourth.PokerCombinations;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Exceptions;
using OOP_ICT.Third.Exceptions;
using OOP_ICT.Third.Exceptions.JoinGame;

namespace OOP_ICT.Fourth;

public class PokerGame
{
    private PokerCasino _casino;
    private PlayerDealer _dealer;

    private Dictionary<Player, uint> _bets = new();
    private List<Player> _participants = new();
    private List<Card> _tableCards = new();
    
    public PokerGameStateEnum State { get; private set; } = PokerGameStateEnum.NotStarted;

    public PokerGame(PokerCasino casino, Player dealer)
    {
        _casino = casino;
        _dealer = new PlayerDealer(player: dealer);
        _dealer.ShuffleDeck();
    }

    public ReadOnlyCollection<Card> TableCards => _tableCards.AsReadOnly();

    
    public void Join(Player player)
    {
        if (player == _dealer.Player)
        {
            throw new InvalidRoleOperationException($"Dealer can't join game");
        }
        
        if (State != PokerGameStateEnum.NotStarted)
        {
            throw new WrongGameStateException($"Game already started");
        }

        if (_participants.Contains(player))
        {
            throw new AlreadyJoinedGameException($"{player} already joined the game");
        }

        if (!_casino.HasSufficientFunds(player, PokerGameConfig.BetToJoin))
        {
            throw new NotEnoughFundsException($"{player} doesn't have enough chips to join game");
        }

        // Игрок ставит ставку (блайнд), чтобы присоединиться к игре
        _casino.RemoveChips(player, PokerGameConfig.BetToJoin);
        _bets.Add(player, PokerGameConfig.BetToJoin);
        _participants.Add(player);
    }
    
    private void DealCardsToPlayers()
    {
        foreach (var player in _participants)
        {
            var cardsDealt = _dealer.DealCards(PokerGameConfig.PlayerCardsAmount);
            player.TakeCards(cardsDealt);
        }
        
        State++;
    }

    private void TakeCardsFromPlayers()
    {
        foreach (var player in _participants)
        {
            var playerCards = player.GiveCards();
            _dealer.TakeCards(playerCards);
        }
    }

    private void DealCardsToTable()
    {
        var amountCardsToDeal = State switch
        {
            PokerGameStateEnum.NotStarted => PokerGameConfig.FlopCardsAmount,
            PokerGameStateEnum.Flop => PokerGameConfig.TurnCardsAmount,
            PokerGameStateEnum.Turn => PokerGameConfig.RiverCardsAmount,
            _ => throw new NotImplementedException()
        };
        
        var dealtCards = _dealer.DealCards(amountCardsToDeal);
        _tableCards.AddRange(dealtCards);
    }

    public void ChangeDealer(Player newDealer)
    {
        if (State != PokerGameStateEnum.Finished)
        {
            throw new WrongGameStateException($"Game is not finished");
        }

        if (_dealer.Player == newDealer)
        {
            throw new AlreadyWasDealerException("This player was dealer in previous game");
        }
        
        _dealer = new PlayerDealer(player: newDealer);
        _dealer.ShuffleDeck();

        State = PokerGameStateEnum.NotStarted;
    }

    public void PlaceBet(Player player, uint bet)
    {
        if (player == _dealer.Player)
        {
            throw new InvalidRoleOperationException($"Dealer can't place bets");
        }
        
        var statesWhenCanBet = new[]
        {
            PokerGameStateEnum.Flop,
            PokerGameStateEnum.Turn,
            PokerGameStateEnum.River
        };
        
        if (!statesWhenCanBet.Contains(State))
        {
            throw new WrongGameStateException($"Players can't bet in '{State}' state of the game");
        }

        if (!_participants.Contains(player))
        {
            throw new DidntJoinGameException($"{player} doesn't participate in game");
        }

        if (!_casino.HasSufficientFunds(player, bet))
        {
            throw new NotEnoughFundsException($"{player} doesn't have enough chips");
        }

        if (bet < PokerGameConfig.MinimalBet)
        {
            throw new BetTooSmallException($"Bet should be at least {PokerGameConfig.MinimalBet} chip");
        }
        
        _casino.RemoveChips(player, bet);
        _bets[player] += bet;
    }

    public void Fold(Player player)
    {
        if (player == _dealer.Player)
        {
            throw new InvalidRoleOperationException($"Dealer can't fold");
        }
        
        var statesWhenCanFold = new[]
        {
            PokerGameStateEnum.Flop,
            PokerGameStateEnum.Turn,
            PokerGameStateEnum.River
        };
        if (!statesWhenCanFold.Contains(State))
        {
            throw new WrongGameStateException($"Players can't fold at '{State}' state of the game");
        }

        var playerCards = player.GiveCards();
        _dealer.TakeCards(playerCards);
        _dealer.ShuffleDeck();
        _participants.Remove(player);
    }

    public void NextTurn()
    {
        var statesWhenCardsDealt = new[]
        {
            PokerGameStateEnum.NotStarted,
            PokerGameStateEnum.Flop,
            PokerGameStateEnum.Turn
        };
        if (!statesWhenCardsDealt.Contains(State))
        {
            throw new WrongGameStateException($"Cards can't be dealt at '{State}' state of the game");
        }
        
        // Раздаем карты игрокам, если начинается стадия Flop
        if (State == PokerGameStateEnum.NotStarted)
        {
            DealCardsToPlayers();
        }
        
        // Добавляем карты на стол
        DealCardsToTable();
        
        State++;
    }

    public void Finish()
    {
        if (State == PokerGameStateEnum.NotStarted)
        {   
            throw new WrongGameStateException($"Game has not started yet");
        }
        
        // Распределение ставок среди победителей
        var winners = PokerCombinationHelper.DetermineWinners(
            players: _participants, 
            tableCards: _tableCards
        );
        DistributeWinnings(winners);
        
        
        // Собирание карт и очищение участников для след. игры
        TakeCardsFromPlayers();
        _participants.Clear();
        
        State = PokerGameStateEnum.Finished;
    }
    
    private void DistributeWinnings(List<Player> winners)
    {   
        var totalBets = _bets.Values.Select(b => (long)b).Sum();
        var winnersPrize = (uint) (totalBets / winners.Count);
        
        foreach (var winner in winners)
        {
            _casino.AddChips(winner, winnersPrize);
        }

        _bets = new Dictionary<Player, uint>();
    }
}
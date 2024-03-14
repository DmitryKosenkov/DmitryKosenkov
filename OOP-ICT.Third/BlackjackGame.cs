using System.Collections.ObjectModel;
using OOP_ICT.Models;
using OOP_ICT.Second.Models;
using OOP_ICT.Second.Models.Exceptions;
using OOP_ICT.Third.Exceptions;
using OOP_ICT.Third.Exceptions.GameState;
using OOP_ICT.Third.Exceptions.JoinGame;

namespace OOP_ICT.Third;

public class BlackjackGame
{
    private Dictionary<Player, uint> _bets = new();
    private BlackjackCasino _casino;
    private Dealer _dealer;
    private List<Card> _dealerCards = new();
    public readonly uint MinimalBet;
    private bool _isGameStarted = false;

    public BlackjackGame(BlackjackCasino casino, Dealer dealer, uint minimalBet = 1)
    {
        _casino = casino;
        _dealer = dealer;
        MinimalBet = minimalBet;
    }

    public ReadOnlyCollection<Card> DealerCards => _dealerCards.AsReadOnly();

    public uint FindPlayerBet(Player player)
    {
        var exist = _bets.TryGetValue(player, out var bet);
        if (!exist)
        {
            throw new DidntJoinGameException($"{player} doesn't participate in game");
        }

        return bet;
    }

    public void Join(Player player, uint bet)
    {
        if (_isGameStarted)
        {
            throw new GameAlreadyStartedException($"Game already started");
        }

        var exist = _bets.TryGetValue(player, out _);
        if (exist)
        {
            throw new AlreadyJoinedGameException($"{player} already joined game");
        }

        if (!_casino.HasSufficientFunds(player, bet))
        {
            throw new NotEnoughFundsException($"{player} doesn't have enough chips");
        }

        if (bet < MinimalBet)
        {
            throw new BetTooSmallException($"Bet should be at least {MinimalBet} chip");
        }

        _bets.Add(player, bet);
    }

    public void Start()
    {
        if (_isGameStarted)
        {
            throw new GameAlreadyStartedException($"Game already started");
        }

        foreach (var (player, _) in _bets)
        {
            var cards = _dealer.DealCards(BlackjackGameConfig.InitialCardDrawAmount);
            player.TakeCards(cards);
        }

        _dealerCards = _dealer.DealCards(1);
        _isGameStarted = true;
    }

    public void Hit(Player player)
    {
        if (!_isGameStarted)
        {
            throw new GameNotStartedException($"Game has not started yet");
        }

        // Убедиться что человек участвует в игре
        FindPlayerBet(player);

        var cards = _dealer.DealCards(1);
        player.TakeCards(cards);
    }

    public void Finish()
    {
        if (!_isGameStarted)
        {
            throw new GameNotStartedException($"Game has not started yet");
        }

        // Дилер добирает карты
        while (ScoreHelper.CalculateHandScore(_dealerCards) < BlackjackGameConfig.DealerDrawMinimalScore)
        {
            var additionalCard = _dealer.DealCards(1).Single();
            _dealerCards.Add(additionalCard);
        }

        var dealerScore = ScoreHelper.CalculateHandScore(_dealerCards);

        // Начисление выигрышей и проигрышей игроков
        foreach (var (player, bet) in _bets)
        {
            var playerScore = ScoreHelper.CalculateHandScore(player.Cards);
            if (playerScore > BlackjackGameConfig.BlackJackScore)
            {
                _casino.RemoveChips(player, bet);
            }
            else if (dealerScore > BlackjackGameConfig.BlackJackScore)
            {
                if (
                    player.Cards.Count == BlackjackGameConfig.InitialCardDrawAmount &&
                    playerScore == BlackjackGameConfig.BlackJackScore
                )
                {
                    _casino.HandleBlackjack(player, bet);
                }
                else
                {
                    _casino.AddChips(player, bet);
                }
            }
            else
            {
                if (dealerScore < playerScore)
                {
                    _casino.AddChips(player, bet);
                }
                else if (dealerScore > playerScore)
                {
                    _casino.RemoveChips(player, bet);
                }
            }

            var playerCards = player.GiveCards();
            _dealer.TakeCards(playerCards);
        }

        ResetGame();
    }

    private void ResetGame()
    {
        _dealer.TakeCards(_dealerCards);
        _dealerCards = new List<Card>();
        _bets = new Dictionary<Player, uint>();
        _isGameStarted = false;
    }
}
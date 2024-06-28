
using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Fourth;

public class PokerTable
{
    private List<Player> _players;
    private Dictionary<Player, List<Card>> _cardsOfPlayers;
    private Dictionary<Player, decimal> _betsOfPlayers;
    private List<Card> _openCards;
    private HandEvaluator _handEvaluator;

    public PokerTable(List<Player> players)
    {
        _players = players;
        _cardsOfPlayers = new Dictionary<Player, List<Card>>();
        _betsOfPlayers = new Dictionary<Player, decimal>();
        _openCards = new List<Card>();
        _handEvaluator = new HandEvaluator();

        foreach (Player player in _players)
        {
            if (player is null)
                throw new PlayerIsNullException("Player can not be null");
            _cardsOfPlayers.Add(player, new List<Card>());
            _betsOfPlayers.Add(player, 0);
        }
    }

    public void AddCardToPlayer(Player player, Card card)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (card is null)
            throw new CardIsNullException("Card can not be null");

        List<Card> cardsOfPlayer;
        _cardsOfPlayers.TryGetValue(player, out cardsOfPlayer);

        if (cardsOfPlayer is null)
            throw new NotFoundException($"Player with id {player.GetId()} not found");

        cardsOfPlayer.Add(card);
    }

    public void RemovePlayer(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (!_players.Contains(player))
            throw new NotFoundException($"Player with id {player.GetId()} not found");

        _players.Remove(player);
        _cardsOfPlayers.Remove(player);
        _betsOfPlayers.Remove(player);
    }

    public void MakeBet(Player player, decimal bet)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (bet < 0)
            throw new DepositValueException("Bet must be a positive number");

        if (_betsOfPlayers.TryGetValue(player, out decimal previousBet))
            _betsOfPlayers[player] = previousBet + bet;
        else
            throw new NotFoundException($"Player with id {player.GetId()} not found");
    }

    public void AddOpenCard(Card card)
    {
        if (card is null)
            throw new CardIsNullException("Player can not be null");
        _openCards.Add(card);
    }

    public int GetHandRank(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");

        List<Card> cardsOfPlayer;
        _cardsOfPlayers.TryGetValue(player, out cardsOfPlayer);

        if (cardsOfPlayer is null)
            throw new NotFoundException($"Player with id {player.GetId()} not found");

        foreach (Card card in _openCards)
            cardsOfPlayer.Add(card);
        return _handEvaluator.EvaluatePokerHand(cardsOfPlayer);
    }
    public decimal GetBet(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (!_betsOfPlayers.ContainsKey(player))
            throw new NotFoundException($"Player with id {player.GetId()} not found");

        decimal bet;
        if (_betsOfPlayers.TryGetValue(player, out bet))
            return bet;
        return 0;
    }

    public decimal GetTotalBets()
    {
        decimal total = 0;
        foreach (Player player in _players)
            total += GetBet(player);
        return total;
    }
}

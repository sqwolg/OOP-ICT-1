using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third.Exceptions;

namespace OOP_ICT.Third;

public class BlackjackTable
{
    private List<Player> _players;
    private Dictionary<Player, List<Card>> _cardsOfPlayers;
    private Dictionary<Player, decimal> _betsOfPlayers;
    private List<Card> _cardsOfDealer;

    public BlackjackTable(List<Player> players)
    {
        _players = players;
        _cardsOfPlayers = new Dictionary<Player, List<Card>>();
        _betsOfPlayers = new Dictionary<Player, decimal>();
        _cardsOfDealer = new List<Card>();

        foreach (Player player in _players)
        {
            if (player is null)
                throw new PlayerIsNullException("Player can not be null");
            _cardsOfPlayers.Add(player, new List<Card>());
            _betsOfPlayers.Add(player,  0);
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

    public void AddCardToDealer(Card card)
    {
        if (card is null)
            throw new CardIsNullException("Card can not be null");
        _cardsOfDealer.Add(card);
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

    private int GetPoints(List<Card> cards)
    {
        int points = 0;
        foreach (Card card in cards)
            points += card.Value switch
            {
                CardValues.Ace => (points > 10) ? 1 : 11,
                CardValues.King => 10,
                CardValues.Queen => 10,
                CardValues.Jack => 10,
                CardValues.Ten => 10,
                CardValues.Nine => 9,
                CardValues.Eight => 8,
                CardValues.Seven => 7,
                CardValues.Six => 6,
                CardValues.Five => 5,
                CardValues.Four => 4,
                CardValues.Three => 3,
                CardValues.Two => 2,
                _ => throw new ArgumentException("Invalid card value"),
            };

        return points;
    }

    public int GetCurrentPoints(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");

        List<Card> cardsOfPlayer;
        _cardsOfPlayers.TryGetValue(player, out cardsOfPlayer);

        if (cardsOfPlayer is null)
            throw new NotFoundException($"Player with id {player.GetId()} not found");

        return GetPoints(cardsOfPlayer);
    }

    public int GetCurrentPoints(Dealer dealer)
    {
        if (dealer is null)
            throw new DealerIsNullException("Dealer can not be null");

        return GetPoints(_cardsOfDealer);
    }

    public int GetCardsAmount(Player player)
    {
        List<Card> cardsOfPlayer;
        _cardsOfPlayers.TryGetValue(player, out cardsOfPlayer);

        if (cardsOfPlayer is null)
            throw new NotFoundException($"Player with id {player.GetId()} not found");

        return cardsOfPlayer.Count;
    }

    public int GetCardsAmount(Dealer dealer)
    {
        if (dealer is null)
            throw new DealerIsNullException("Dealer can not be null");

        return _cardsOfDealer.Count;
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
}

using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Fourth;

public class PokerGame
{
    private List<Player> _activePlayers;
    private List<Player> _allPlayers;
    private int _currentDealerIndex = 0;
    private DealerAdapter _currentDealer;
    private BlackjackCasino _casino;

    private const int PreFlopCardsAmount = 2;
    private const int FlopCardsAmount = 3;

    public int PlayersAmount => _allPlayers.Count;

    public PokerGame(DealerAdapter dealer, BlackjackCasino casino)
    {
        _activePlayers = new List<Player>();
        _allPlayers = new List<Player>();
        _currentDealer = dealer;
        _casino = casino;
    }
    public void Join(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (_allPlayers.Any(playerInList => playerInList.GetId() == player.GetId()))
            throw new AlreadyExistsException($"Player with id {player.GetId()} is already in the game");
        _allPlayers.Add(player);
    }
    public void StartNewGame()
    {
        _activePlayers = _allPlayers.ToList();
        _currentDealer.SetDealer(_activePlayers.ElementAt(_currentDealerIndex));
        var table = new PokerTable(_activePlayers);
        _currentDealer.ShuffleDeck();

        DealCards(table); // preflop
        GetPlayersBets(table);

        for (int i = 0; i < FlopCardsAmount; i++) // flop
            OpenNextCard(table);
        GetPlayersBets(table);

        OpenNextCard(table); // turn
        GetPlayersBets(table);

        OpenNextCard(table); // river
        GetPlayersBets(table);

        List<Player> winners = GetWinner(table);
        HandleGameResults(table, winners);

        _currentDealerIndex = (_currentDealerIndex + 1) % _activePlayers.Count;
        _currentDealer.SetDealer(_activePlayers.ElementAt(_currentDealerIndex));
    }

    public void DealCards(PokerTable table)
    {
        foreach(Player player in _activePlayers)
        {
            for (int i = 0; i < PreFlopCardsAmount; i++)
            {
                var cardForPlayer = _currentDealer.DealCard();
                table.AddCardToPlayer(player, cardForPlayer);
            }
        }
    }

    public void GetPlayersBets(PokerTable table)
    {
        var minBet = 10;
        foreach(Player player in _activePlayers)
        {
            if (!_casino.HasEnoughChips(player, minBet))
                player.SetStrategy(new FoldStrategy());

            var playerBet = player.GetBet(minBet);
            if (playerBet == 0) // player folded
            {
                table.RemovePlayer(player); 
                _activePlayers.Remove(player);
            }
            else table.MakeBet(player, playerBet);
        }
    }

    public void OpenNextCard(PokerTable table)
    {
        var card = _currentDealer.DealCard();
        table.AddOpenCard(card);
    }

    public List<Player> GetWinner(PokerTable table)
    {
        var maxHandRank = 0;
        var winners = new List<Player>();
        foreach (Player player in _activePlayers)
        {
            var playerHandRank = table.GetHandRank(player);
            if (playerHandRank > maxHandRank)
                maxHandRank = playerHandRank;
        }
        foreach (Player player in _activePlayers)
        {
            var playerHandRank = table.GetHandRank(player);
            if (playerHandRank == maxHandRank)
                winners.Add(player);
        }
        return winners;
    }

    public void HandleGameResults(PokerTable table, List<Player> winners)
    {
        decimal totalBets = table.GetTotalBets();
        foreach (Player player in _activePlayers)
        {
            if (winners.Contains(player))
                _casino.AddWinnings(player, totalBets / winners.Count);
            else
                _casino.AddLosses(player, table.GetBet(player));
        }
    }
}

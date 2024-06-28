using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Third;

public class BlackjackGame
{
    private List<Player> _players;
    private Dealer _dealer;
    private BlackjackCasino _casino;

    public int PlayersAmount => _players.Count;

    // how many cards dealer gives to each player at the beginnig of the round
    private const int CardsPerPlayer = 2;
    // when the dealer must stop taking cards
    private const int MaxDealerPoints = 17;
    // when players stop taking cards based on their strategy in this case
    private const int MaxPlayerPoints = 18;

    public BlackjackGame(Dealer dealer, BlackjackCasino casino)
    {
        _dealer = dealer;
        _players = new List<Player>();
        _casino = casino;
    }

    public void Join(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (_players.Any(playerInList => playerInList.GetId() == player.GetId()))
            throw new AlreadyExistsException($"Player with id {player.GetId()} is already in the game");
        _players.Add(player);
    }

    public void PlayRound()
    {
        var table = new BlackjackTable(_players);
        MakeBets(table);
        DealInitialCards(table);
        MakePlayerMoves(table);
        CalculateWinners(table);
    }

    private void MakeBets(BlackjackTable table)
    {
        var bet = 10;
        foreach (var player in _players)
        {
            if (!_casino.HasEnoughChips(player, bet))
                throw new DepositValueException("Not enough chips to bet");
            table.MakeBet(player, bet);
        }

    }

    private void DealInitialCards(BlackjackTable table)
    {
        for (int i = 0; i < CardsPerPlayer; i++)
        {
            foreach (var player in _players)
            {
                var playerCard = _dealer.DealCard();
                table.AddCardToPlayer(player, playerCard);
            }

            // deal one card for each player then one card for the dealer
            if (i == 0)
            {
                var dealerCard = _dealer.DealCard();
                table.AddCardToDealer(dealerCard);
            }
        }
    }

    private void MakePlayerMoves(BlackjackTable table)
    {
        foreach(var player in _players)
        {
            while (table.GetCurrentPoints(player) < MaxPlayerPoints)
            {
                var playerCard = _dealer.DealCard();
                table.AddCardToPlayer(player, playerCard);
            }
        }
    }

    private void CalculateWinners(BlackjackTable table)
    {
        while (table.GetCurrentPoints(_dealer) < MaxDealerPoints)
        {
            var dealerCard = _dealer.DealCard();
            table.AddCardToDealer(dealerCard);
        }

        foreach (var player in _players)
        {
            var playerPoints = table.GetCurrentPoints(player);
            var dealerPoints = table.GetCurrentPoints(_dealer);
            var playerCardsAmount = table.GetCardsAmount(player);
            var dealerCardsAmount = table.GetCardsAmount(_dealer);
            var playerBet = table.GetBet(player);

            if (playerPoints > 21)
            {
                _casino.AddLosses(player, playerBet);
            }
            else if (dealerPoints == 21 && dealerCardsAmount == 2)
            {
                if (playerPoints < 21 || playerCardsAmount > 2)
                    _casino.AddLosses(player, playerBet);
            }
            else if (playerPoints == 21 && playerCardsAmount == 2)
            {
                _casino.AddBlackjack(player, playerBet);
            }
            else if (dealerPoints > 21)
            {
                _casino.AddWinnings(player, playerBet);
            }
        }
    }
}

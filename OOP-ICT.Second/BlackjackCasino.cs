using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second;
public class BlackjackCasino
{
    private Dictionary<Player, CasinoAccount> players;
    private CasinoAccountCreator creator;

    public BlackjackCasino()
    {
        players = new Dictionary<Player, CasinoAccount>();
        creator = new CasinoAccountCreator();
    }
    public void RegisterPlayer(Player player)
    {
        if(player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (players.ContainsKey(player))
            throw new AlreadyExistsException($"Player with id {player.GetId()} already exists");
        players.Add(player, creator.CreateAccount(player));
    }
    private void ValidatePlayer(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (!players.ContainsKey(player))
            throw new NotFoundException($"Player with id {player.GetId()} not found");
    }
    public void AddWinnings(Player player, decimal bet)
    {
        ValidatePlayer(player);
        CasinoAccount account;
        players.TryGetValue(player, out account);
        if (account is not null)
            account.Add(bet);
    }

    public void AddLosses(Player player, decimal bet)
    {
        ValidatePlayer(player);
        CasinoAccount account;
        players.TryGetValue(player, out account);
        if (account is not null)
            account.Withdraw(bet);
    }

    public void AddBlackjack(Player player, decimal bet)
    {
        ValidatePlayer(player);
        CasinoAccount account;
        players.TryGetValue(player, out account);
        if (account is not null)
            account.Add(bet * 1.5m);
    }

    public bool HasEnoughChips(Player player, decimal amountOfChips)
    {
        ValidatePlayer(player);
        CasinoAccount account;
        players.TryGetValue(player, out account);
        bool result = false;
        if (account is not null)
            return account.HasEnoughBalance(amountOfChips);
        return result;
    }
}


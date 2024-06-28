using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second;

public class Bank
{
    private Dictionary<Player, BankAccount> accounts;
    private BankAccountCreator creator;
    public Bank()
    {
        accounts = new Dictionary<Player, BankAccount>();
        creator = new BankAccountCreator();
    }

    public void CreateAccount(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (accounts.ContainsKey(player))
            throw new AlreadyExistsException($"Player with id {player.GetId()} already exists");
        accounts.Add(player, creator.CreateAccount(player));
    }

    private void ValidatePlayer(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (!accounts.ContainsKey(player))
            throw new NotFoundException($"Player with id {player.GetId()} not found");
    }
    public void Add(Player player, decimal amountOfMoney)
    {
        ValidatePlayer(player);
        BankAccount account;
        accounts.TryGetValue(player, out account);
        if (account is not null)
            account.Add(amountOfMoney);
    }

    public void Withdraw(Player player, decimal amountOfMoney)
    {
        ValidatePlayer(player);
        BankAccount account;
        accounts.TryGetValue(player, out account);
        if (account is not null)
            account.Withdraw(amountOfMoney);
    }

    public bool HasEnoughMoney(Player player, decimal amountOfMoney)
    {
        ValidatePlayer(player);
        BankAccount account;
        accounts.TryGetValue(player, out account);
        bool result = false;
        if (account is not null)
            result  = account.HasEnoughBalance(amountOfMoney);
        return result;
    }
}

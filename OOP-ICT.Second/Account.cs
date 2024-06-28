using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Second;

public abstract class Account
{
    private Player _player;
    private decimal _balance;
    public Account(Player player)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        _player = player;
        _balance = 0;

    }
    public void Add(decimal amount)
    {
        if (amount < 0)
            throw new DepositValueException("Amount must be positive or zero");
        _balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
            throw new DepositValueException("Amount must be positive or zero");
        if (!HasEnoughBalance(amount))
            throw new NotEnoughBalanceException("Not enough balance to withdraw");
        _balance -= amount;
    }

    public bool HasEnoughBalance(decimal amount)
    {
        if (amount < 0)
            throw new DepositValueException("Amount must be positive or zero");
        return _balance >= amount;
    }
}

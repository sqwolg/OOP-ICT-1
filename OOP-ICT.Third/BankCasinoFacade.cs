using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;

namespace OOP_ICT.Third;

public class BankCasinoFacade
{
    private Bank _bank;
    private BlackjackCasino _casino;

    public BankCasinoFacade(Bank bank, BlackjackCasino casino)
    {
        _bank = bank;
        _casino = casino;
    }

    public void BuyChips(Player player, decimal amount)
    {
        if (player is null)
            throw new PlayerIsNullException("Player can not be null");
        if (amount < 0)
            throw new DepositValueException("Amount must be a positive number");
        if (!_bank.HasEnoughMoney(player, amount))
            throw new NotEnoughBalanceException("Not enough money to buy chips");

        _bank.Withdraw(player, amount);
        _casino.AddWinnings(player, amount);
    }
}

namespace OOP_ICT.Second;

public class BankAccount : Account
{
    private Player _player;
    private decimal _balance;
    private static int lastAccountId = 0;
    private int accountId;

    public BankAccount(Player player) : base(player)
    {
        accountId = ++lastAccountId;
    }
}

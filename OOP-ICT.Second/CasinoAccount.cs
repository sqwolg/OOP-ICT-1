namespace OOP_ICT.Second;

public class CasinoAccount : Account
{
    private Player _player;
    private decimal _balance;
    private static int _lastAccountId = 0;
    private int _accountId;

    public CasinoAccount(Player player) : base(player)
    {
        _accountId = ++_lastAccountId;
    }

}

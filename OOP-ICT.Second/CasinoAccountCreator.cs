namespace OOP_ICT.Second;

internal class CasinoAccountCreator : AccountCreator
{
    public override CasinoAccount CreateAccount(Player player)
    {
        return new CasinoAccount(player);
    }
}

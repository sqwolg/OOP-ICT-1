namespace OOP_ICT.Second;

public class BankAccountCreator : AccountCreator
{
    public override BankAccount CreateAccount(Player player)
    {
        return new BankAccount(player);
    }
}

namespace OOP_ICT.Fourth;

public class CallStrategy : IStrategy
{
    public decimal GetBet(decimal minBet)
    {
        return minBet;
    }
}

using OOP_ICT.Fourth;

namespace OOP_ICT.Second;

public class Player
{
    private static int lastPlayerId = 0;
    private readonly int _playerId;
    private IStrategy _strategy;
    public Player()
    {
        _playerId = ++lastPlayerId;
        _strategy = new CallStrategy();
    }
    public int GetId() { return _playerId; }

    public decimal GetBet(decimal minBet)
    {
        return _strategy.GetBet(minBet);
    }

    public void SetStrategy(IStrategy strategy)
    {
        _strategy = strategy;
    }
}

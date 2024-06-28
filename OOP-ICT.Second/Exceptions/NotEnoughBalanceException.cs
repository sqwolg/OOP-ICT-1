namespace OOP_ICT.Second.Exceptions;

public class NotEnoughBalanceException : ArgumentException
{
    public NotEnoughBalanceException(string message) : base(message)
    {
    }
}

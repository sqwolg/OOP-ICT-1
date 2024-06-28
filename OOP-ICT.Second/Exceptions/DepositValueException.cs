namespace OOP_ICT.Second.Exceptions;

public class DepositValueException : ArgumentException
{
    public DepositValueException(string message) : base(message)
    {
    }
}

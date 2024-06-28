namespace OOP_ICT.Second.Exceptions;

public class PlayerIsNullException : ArgumentException
{
    public PlayerIsNullException(string message) : base(message)
    {
    }
}

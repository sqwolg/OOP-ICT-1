namespace OOP_ICT.Second.Exceptions;

public class AlreadyExistsException : ArgumentException
{
    public AlreadyExistsException(string message) : base(message)
    {
    }
}

namespace OOP_ICT.Second.Exceptions;

public class NotFoundException : ArgumentException
{
    public NotFoundException(string message) : base(message)
    {
    }
}

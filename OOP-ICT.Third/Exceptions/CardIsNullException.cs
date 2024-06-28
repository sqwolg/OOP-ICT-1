namespace OOP_ICT.Third.Exceptions;

public class CardIsNullException : ArgumentNullException
{
    public CardIsNullException(string message) : base(message)
    {
    }
}

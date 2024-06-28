namespace OOP_ICT.Models;

public class Card
{
    public CardSuits Suit { get; private set; }
    public CardValues Value { get; private set; }

    public Card(CardSuits suit, CardValues value)
    {
        Suit = suit;
        Value = value;
    }
}

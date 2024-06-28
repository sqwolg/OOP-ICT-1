namespace OOP_ICT.Models;

public class CardDeck
{
    private List<Card> _cards = new();
    private int topCardIndex = 0;
    public int Length => _cards.Count;
    public CardDeck()
    {
        foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
            foreach (CardValues value in Enum.GetValues(typeof(CardValues)))
                _cards.Add(new Card(suit, value));
    }

    public void Shuffle()
    {
        var half = Length / 2;
        List<Card> temp = new();
        for (var i = 0; i < half; i++)
        {
            try
            {
                temp.Add(GetCardAt(i + half));
                temp.Add(GetCardAt(i));
            }
            catch (IndexOutOfRangeException ex)
            { 
                Console.WriteLine("Deck shuffle failed:");
                Console.WriteLine(ex.Message);
                return;
            }
        }
        _cards = temp;
    }

    public Card GetCardAt(int index)
    {
        if (index >= 0 && index < Length)
            return _cards[index];
        else
            throw new IndexOutOfRangeException($"Index {index} is invalid for a deck of length {Length}");
    }

    public Card GetTopCard()
    {
        var topCard = GetCardAt(topCardIndex);
        topCardIndex = (topCardIndex + 1) % Length;

        return topCard;
    }

    public IReadOnlyList<Card> GetCards() { return _cards; }
}
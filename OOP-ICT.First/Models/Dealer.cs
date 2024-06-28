namespace OOP_ICT.Models;

public class Dealer
{
    private CardDeck _cardDeck;
    // casinos usually do 5-7 perfect shuffles in order to achieve randomly shuffled deck
    // if the deck would be shuffled 8 times it would return to the original order
    private const int ShuffleIteration = 6;

    public Dealer(CardDeck cardDeck)
    {
        _cardDeck = cardDeck;
    }

    public IReadOnlyList<Card> GetCards()
    {
        return _cardDeck.GetCards();
    }

    public void ShuffleDeck()
    {
        for (var i = 0; i < ShuffleIteration; i++)
            _cardDeck.Shuffle();
    }

     public Card DealCard()
     {
        return _cardDeck.GetTopCard();
     }
    
}
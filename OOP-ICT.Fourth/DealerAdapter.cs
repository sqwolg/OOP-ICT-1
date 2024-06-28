using OOP_ICT.Models;
using OOP_ICT.Second;

namespace OOP_ICT.Fourth;

public class DealerAdapter
{
    private CardDeck _cardDeck;
    private Player _currentDealer;

    // casinos usually do 5-7 perfect shuffles in order to achieve randomly shuffled deck
    // if the deck would be shuffled 8 times it would return to the original order
    private const int ShuffleIteration = 6;

    public DealerAdapter(CardDeck cardDeck)
    {
        _cardDeck = cardDeck;
    }

    public void SetDealer(Player newDealer)
    {
        _currentDealer = newDealer;
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

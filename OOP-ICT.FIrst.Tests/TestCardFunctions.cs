using OOP_ICT.Models;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    /// <summary>
    /// Тесты пишутся из трех частей итог - данные - что вернуло 
    /// </summary>

    [Fact]
    public void AreEqual_CardsHaveSameSuit_ReturnTrue()
    {
        var cardTen = new Card(CardSuits.Spades, CardValues.Ten);
        var cardAce = new Card(CardSuits.Spades, CardValues.Ace);

        Assert.Equal(cardTen.Suit, cardAce.Suit);
    }

    [Fact]
    public void AreEquals_CardsAreInCorrectOrder_ReturnTrue()
    {
        var deck = new CardDeck();
        int currentCardIndex = 0;

        foreach (CardSuits suit in Enum.GetValues(typeof(CardSuits)))
            foreach (CardValues value in Enum.GetValues(typeof(CardValues)))
            {
                Card cardToCheck = deck.GetCardAt(currentCardIndex);
                Assert.Equal(cardToCheck.Suit, suit);
                Assert.Equal(cardToCheck.Value, value);
                currentCardIndex++;
            }
    }
    [Fact]
    public void AreEquals_DecksAreTheSame_ReturnTrue()
    {
        var deck1 = new CardDeck();
        var deck2 = new CardDeck();

        Assert.Equal(deck1.Length, deck2.Length);

        for (int i = 0; i < deck1.Length; i++)
        {
            Card cardFromDeck1 = deck1.GetCardAt(i);
            Card cardFromDeck2 = deck2.GetCardAt(i);

            Assert.Equal(cardFromDeck1.Suit, cardFromDeck2.Suit);
            Assert.Equal(cardFromDeck1.Value, cardFromDeck2.Value);
        }
    }

    [Fact]
    public void AreNotEquals_DeckWasShuffled_ReturnTrue()
    {
        var deckBeforeShuffle = new CardDeck();
        var deckAfterShuffle = new CardDeck();
        deckAfterShuffle.Shuffle();
        int differentCardsCount = 0;

        for (int i = 0; i < deckBeforeShuffle.Length; i++)
        {
            Card cardFromDeckBeforeShuffle = deckBeforeShuffle.GetCardAt(i);
            Card cardFromDeckAfterShuffle = deckAfterShuffle.GetCardAt(i);

            if (cardFromDeckBeforeShuffle.Suit != cardFromDeckAfterShuffle.Suit ||
                cardFromDeckBeforeShuffle.Value != cardFromDeckAfterShuffle.Value)
            {
                differentCardsCount++;
            }
        }
        Assert.NotEqual(0, differentCardsCount);
    }

    [Fact]
    public void AreEquals_CardWithInvalidIndexIsNull_ReturnTrue()
    {
        var deck = new CardDeck();

        Assert.Throws<IndexOutOfRangeException>(() => deck.GetCardAt(-1));
        Assert.Throws<IndexOutOfRangeException>(() => deck.GetCardAt(53));
    }

    [Fact]
    public void AreEquals_DeckHas52Cards_ReturnTrue()
    {
        var deck = new CardDeck();
        const int DECK_LENGTH = 52;

        Assert.Equal(DECK_LENGTH, deck.Length);
    }
}
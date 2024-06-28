using OOP_ICT.Models;

public class HandEvaluator
{

    IEnumerable<IEnumerable<T>> GenerateCombinations<T>(List<T> elements, int combinationLength)
    {
        return GenerateCombinationsHelper(elements, combinationLength, 0);
    }

    IEnumerable<IEnumerable<T>> GenerateCombinationsHelper<T>(List<T> elements, int remainingLength, int startIndex)
    {
        if (remainingLength == 0)
        {
            yield return Enumerable.Empty<T>();
        }
        else
        {
            for (int i = startIndex; i <= elements.Count - remainingLength; i++)
            {
                var currentElement = elements[i];
                var remainingElements = elements.Skip(i + 1).ToList();

                foreach (var combination in GenerateCombinationsHelper(remainingElements, remainingLength - 1, 0))
                {
                    yield return new[] { currentElement }.Concat(combination);
                }
            }
        }
    }

    public int EvaluatePokerHand(List<Card> cards)
    {
        var cardsCombinations = GenerateCombinations(cards, 5);
        var maxScore = 0;
        foreach(var comb in cardsCombinations)
        {
            var score = EvaluatePokerHandHelper(comb.ToList());
            if (score > maxScore)
                maxScore = score;
        }
        return maxScore;
    }

    public int EvaluatePokerHandHelper(List<Card> cards)
    {
        var orderedCards = cards.OrderByDescending(card => ConvertCardValue(card.Value)).ToList();

        if (IsRoyalFlush(orderedCards))
        {
            return GetScore(PokerHand.RoyalFlush, orderedCards);
        }
        if (IsStraightFlush(orderedCards))
        {
            return GetScore(PokerHand.StraightFlush, orderedCards);
        }
        if (IsFourOfAKind(orderedCards))
        {
            return GetScore(PokerHand.FourOfAKind, orderedCards);
        }
        if (IsFullHouse(orderedCards))
        {
            return GetScore(PokerHand.FullHouse, orderedCards);
        }
        if (IsFlush(orderedCards))
        {
            return GetScore(PokerHand.Flush, orderedCards);
        }
        if (IsStraight(orderedCards))
        {
            return GetScore(PokerHand.Straight, orderedCards);
        }
        if (IsThreeOfAKind(orderedCards))
        {
            return GetScore(PokerHand.ThreeOfAKind, orderedCards);
        }
        if (IsTwoPair(orderedCards))
        {
            return GetScore(PokerHand.TwoPair, orderedCards);
        }
        if (IsOnePair(orderedCards))
        {
            return GetScore(PokerHand.OnePair, orderedCards);
        }

        return GetScore(PokerHand.HighCard, orderedCards);
    }

    private int GetScore(PokerHand hand, List<Card> orderedCards)
    {
        int baseScore = (int)hand * 1000;

        for (int i = 0; i < orderedCards.Count; i++)
        {
            baseScore += orderedCards.Max(card => ConvertCardValue(card.Value)) * 10;
        }

        return baseScore;
    }

    private int ConvertCardValue(CardValues value)
    {
        // CardValues.Two has a value of 13, so i have to do (15 - value) to get the actual value
        return 15 - (int)value;
    }

    private bool IsRoyalFlush(List<Card> cards)
    {
        return IsStraightFlush(cards) && cards[0].Value == CardValues.Ace;
    }

    private bool IsStraightFlush(List<Card> cards)
    {
        return IsStraight(cards) && IsFlush(cards);
    }

    private bool IsFourOfAKind(List<Card> cards)
    {
        return cards.GroupBy(card => card.Value).Any(group => group.Count() == 4);
    }

    private bool IsFullHouse(List<Card> cards)
    {
        return IsThreeOfAKind(cards) && IsOnePair(cards);
    }

    private bool IsFlush(List<Card> cards)
    {
        return cards.GroupBy(card => card.Suit).Count() == 1;
    }

    private bool IsStraight(List<Card> cards)
    {
        for (int i = 0; i < cards.Count - 1; i++)
        {
            if (ConvertCardValue(cards.ElementAt(i).Value) != ConvertCardValue(cards.ElementAt(i + 1).Value) + 1)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsThreeOfAKind(List<Card> cards)
    {
        return cards.GroupBy(card => card.Value).Any(group => group.Count() == 3);
    }

    private bool IsTwoPair(List<Card> cards)
    {
        var pairs = cards.GroupBy(card => card.Value).Where(group => group.Count() == 2).ToList();
        return pairs.Count == 2;
    }

    private bool IsOnePair(List<Card> cards)
    {
        return cards.GroupBy(card => card.Value).Any(group => group.Count() == 2);
    }
}

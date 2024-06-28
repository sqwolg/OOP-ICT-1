using OOP_ICT.Fourth;
using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third.Exceptions;
using Xunit;

public class TestPoker
{
    [Fact]
    public void AreTrue_PlayersCanJoinGame_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var casino = new BlackjackCasino();
        var cardDeck = new CardDeck();
        var dealer = new DealerAdapter(cardDeck);

        casino.RegisterPlayer(player1);
        casino.RegisterPlayer(player2);

        var game = new PokerGame(dealer, casino);
        game.Join(player1);
        Assert.Equal(1, game.PlayersAmount);
        game.Join(player2);
        Assert.Equal(2, game.PlayersAmount);

    }

    [Fact]
    public void ThrowsException_DealerCanAddCardToPlayer_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new PokerTable(players);

        Assert.Throws<PlayerIsNullException>(() => table.AddCardToPlayer(null, new Card(CardSuits.Diamonds, CardValues.Nine)));
        Assert.Throws<CardIsNullException>(() => table.AddCardToPlayer(player1, null));
    }

    [Fact]
    public void AreTrue_CanRemovePlayer_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var player3 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new PokerTable(players);

        Assert.Throws<PlayerIsNullException>(() => table.RemovePlayer(null));
        Assert.Throws<NotFoundException>(() => table.RemovePlayer(player3));
    }

    [Fact]
    public void AreEqual_PlayerCanMakeBetAtTheTable_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new PokerTable(players);

        const decimal Bet1 = 50;
        const decimal Bet2 = 100;

        table.MakeBet(player1, Bet1);
        table.MakeBet(player2, Bet2);

        Assert.Equal(Bet1, table.GetBet(player1));
        Assert.Equal(Bet2, table.GetBet(player2));

        Assert.Throws<DepositValueException>(() => table.MakeBet(player1, Bet1 * (-1)));
        Assert.Throws<NotFoundException>(() => table.MakeBet(new Player(), Bet1));
    }

    [Fact]
    public void AreEqual_CanNotAddNullCard_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new PokerTable(players);

        Assert.Throws<CardIsNullException>(() => table.AddOpenCard(null));
    }

    [Fact]
    public void AreEqual_CardEvaluationsAreCorrect_ReturnTrue()
    {
        // royale flush
        var hand1 = new List<Card>()
        {
            new Card(CardSuits.Spades, CardValues.Ace),
            new Card(CardSuits.Spades, CardValues.King),
            new Card(CardSuits.Spades, CardValues.Queen),
            new Card(CardSuits.Spades, CardValues.Jack),
            new Card(CardSuits.Spades, CardValues.Ten),
            new Card(CardSuits.Diamonds, CardValues.Five),
            new Card(CardSuits.Diamonds, CardValues.Two),
        };
        // four of a kind
        var hand2 = new List<Card>()
        {
            new Card(CardSuits.Spades, CardValues.Ace),
            new Card(CardSuits.Clubs, CardValues.Ace),
            new Card(CardSuits.Diamonds, CardValues.Ace),
            new Card(CardSuits.Hearts, CardValues.Ace),
            new Card(CardSuits.Spades, CardValues.Ten),
            new Card(CardSuits.Diamonds, CardValues.Five),
            new Card(CardSuits.Diamonds, CardValues.Two),
        };

        var evaluator = new HandEvaluator();

        Assert.True(evaluator.EvaluatePokerHand(hand1) > evaluator.EvaluatePokerHand(hand2));
    }
}
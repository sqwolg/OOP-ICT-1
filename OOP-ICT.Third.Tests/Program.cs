using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;
using OOP_ICT.Third;
using OOP_ICT.Third.Exceptions;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    [Fact]
    public void AreTrue_FacadeCanBuyChips_ReturnTrue()
    {
        var bank = new Bank();
        var casino = new BlackjackCasino();
        var bankCasinoFacade = new BankCasinoFacade(bank, casino);
        var player = new Player();

        const decimal ChipsAmount = 500;

        bank.CreateAccount(player);
        casino.RegisterPlayer(player);
        bank.Add(player, ChipsAmount);

        bankCasinoFacade.BuyChips(player, ChipsAmount);

        Assert.True(casino.HasEnoughChips(player, ChipsAmount));
        Assert.False(bank.HasEnoughMoney(player, ChipsAmount));
    }

    [Fact]
    public void AreEqual_PlayersCanJoinGame_ReturnTrue()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);
        var bank = new Bank();
        var casino = new BlackjackCasino();

        var player1 = new Player();
        var player2 = new Player();

        bank.CreateAccount(player1);
        bank.CreateAccount(player2);
        casino.RegisterPlayer(player1);
        casino.RegisterPlayer(player2);

        var game = new BlackjackGame(dealer, casino);

        game.Join(player1);
        game.Join(player2);

        Assert.Equal(2, game.PlayersAmount);
    }

    [Fact]
    public void ThrowsException_PlayersCanMakeBets_ReturnTrue()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);
        var bank = new Bank();
        var casino = new BlackjackCasino();

        var player1 = new Player();
        var player2 = new Player();

        bank.CreateAccount(player1);
        bank.CreateAccount(player2);
        casino.RegisterPlayer(player1);
        casino.RegisterPlayer(player2);

        var game = new BlackjackGame(dealer, casino);

        game.Join(player1);
        game.Join(player2);

        Assert.Throws<DepositValueException>(() => game.PlayRound());
    }

    [Fact]
    public void AreNotEqual_DealerCanDealCards_ReturnTrue()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);

        var card1 = dealer.DealCard();
        var card2 = dealer.DealCard();

        Assert.NotEqual(card1, card2);
        Assert.True(card1.Suit != card2.Suit || card1.Value != card2.Value);
    }

    [Fact]
    public void AreEqual_DealerCanAddCardToTheTable_ReturnTrue()
    {
        var deck = new CardDeck();
        var dealer = new Dealer(deck);
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new BlackjackTable(players);
        var card = new Card(CardSuits.Diamonds, CardValues.Ace);

        Assert.Equal(0, table.GetCardsAmount(dealer));

        table.AddCardToDealer(card);

        Assert.Equal(1, table.GetCardsAmount(dealer));
        Assert.Throws<CardIsNullException>(() => table.AddCardToDealer(null));
    }

    [Fact]
    public void AreEqual_PlayerCanMakeBetAtTheTable_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new BlackjackTable(players);

        const decimal Bet1 = 50;
        const decimal Bet2 = 100;

        table.MakeBet(player1, Bet1);
        table.MakeBet(player2 , Bet2);

        Assert.Equal(Bet1, table.GetBet(player1));
        Assert.Equal(Bet2, table.GetBet(player2));

        Assert.Throws<DepositValueException>(() => table.MakeBet(player1, Bet1 * (-1)));
        Assert.Throws<NotFoundException>(() => table.MakeBet(new Player(), Bet1));
    }

    [Fact]
    public void AreEqual_CalculateCardPoints_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new BlackjackTable(players);

        table.AddCardToPlayer(player1, new Card(CardSuits.Spades, CardValues.Queen));
        table.AddCardToPlayer(player1, new Card(CardSuits.Spades, CardValues.King));

        Assert.Equal(20, table.GetCurrentPoints(player1));
        Assert.Equal(0, table.GetCurrentPoints(player2));

        table.AddCardToPlayer(player1, new Card(CardSuits.Spades, CardValues.Ace));

        Assert.Equal(21, table.GetCurrentPoints(player1));
    }

    [Fact]
    public void AreEqual_PlayerCanAddCardToTheTable_ReturnTrue()
    {
        var player1 = new Player();
        var player2 = new Player();
        var players = new List<Player>() { player1, player2 };
        var table = new BlackjackTable(players);
        var card = new Card(CardSuits.Diamonds, CardValues.Ace);

        Assert.Equal(0, table.GetCardsAmount(player1));
        Assert.Equal(0, table.GetCardsAmount(player2));

        table.AddCardToPlayer(player1, card);

        Assert.Equal(1, table.GetCardsAmount(player1));
        Assert.Equal(0, table.GetCardsAmount(player2));
        Assert.Throws<NotFoundException>(() => table.AddCardToPlayer(new Player(), card));
    }
}
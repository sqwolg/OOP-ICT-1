using NuGet.Frameworks;
using OOP_ICT.Second;
using OOP_ICT.Second.Exceptions;
using Xunit;

namespace OOP_ICT.FIrst.Tests;

public class TestCardFunctions
{
    /// <summary>
    /// Тесты пишутся из трех частей итог - данные - что вернуло 
    /// </summary>

    [Fact]
    public void AreEqual_CasinoRemovedChips_ReturnTrue()
    {
        const decimal winnings = 100;
        var player = new Player();
        var casino = new BlackjackCasino();
        casino.RegisterPlayer(player);

        casino.AddWinnings(player, winnings);
        Assert.True(casino.HasEnoughChips(player, winnings));
        casino.AddLosses(player, winnings);

        Assert.Throws<NotEnoughBalanceException>(() => casino.AddLosses(player, winnings));
        Assert.False(casino.HasEnoughChips(player, winnings));
    }

    [Fact]
    public void AreEqual_CasinoAddedBlackjack_ReturnTrue()
    {
        const decimal winnings = 100;
        const decimal blackjack = 150;

        var player = new Player();
        var casino = new BlackjackCasino();
        casino.RegisterPlayer(player);

        Assert.False(casino.HasEnoughChips(player, blackjack));
        
        casino.AddBlackjack(player, winnings);

        Assert.True(casino.HasEnoughChips(player, blackjack));
    }

    [Fact]
    public void AreEqual_CasinoAddedChips_ReturnTrue()
    {
        const decimal winnings = 100;
        var player = new Player();
        var casino = new BlackjackCasino();
        casino.RegisterPlayer(player);

        Assert.False(casino.HasEnoughChips(player, winnings));

        casino.AddWinnings(player, winnings);

        Assert.Throws<NotEnoughBalanceException>(() => casino.AddLosses(player, winnings * 2));
        Assert.True(casino.HasEnoughChips(player, winnings));
    }

    [Fact]
    public void AreEqual_BankRemovedMoney_ReturnTrue()
    {
        const decimal deposit = 269;
        var player = new Player();
        var bank = new Bank();
        bank.CreateAccount(player);

        Assert.False(bank.HasEnoughMoney(player, deposit));
        bank.Add(player, deposit);
        Assert.True(bank.HasEnoughMoney(player, deposit));
        bank.Withdraw(player, deposit);
        Assert.False(bank.HasEnoughMoney(player, deposit));
    }

    [Fact]
    public void AreEqual_BankCanCheckBalanceMoney_ReturnTrue()
    {
        const decimal deposit = 10;
        var player = new Player();
        var bank = new Bank();
        bank.CreateAccount(player);

        bank.Add(player, deposit);
        Assert.True(bank.HasEnoughMoney(player, deposit));
        bank.Withdraw(player, deposit);
        Assert.False(bank.HasEnoughMoney(player, deposit));
    }

    [Fact]
    public void AreEqual_BankAddedMoney_ReturnTrue()
    {
        const decimal deposit = 169;
        var player = new Player();
        var bank = new Bank();
        bank.CreateAccount(player);

        Assert.False(bank.HasEnoughMoney(player, deposit));

        bank.Add(player, deposit);

        Assert.True(bank.HasEnoughMoney(player, deposit));
    }
}
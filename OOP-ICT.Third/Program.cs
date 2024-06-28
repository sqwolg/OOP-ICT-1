// See https://aka.ms/new-console-template for more information

using OOP_ICT.Models;
using OOP_ICT.Second;
using OOP_ICT.Third;

var deck = new CardDeck();
var dealer = new Dealer(deck);
var bank = new Bank();
var casino = new BlackjackCasino();
var bankCasinoFacade = new BankCasinoFacade(bank, casino);

var player1 = new Player();
var player2 = new Player();

bank.CreateAccount(player1);
bank.CreateAccount(player2);

casino.RegisterPlayer(player1);
casino.RegisterPlayer(player2);

bank.Add(player1, 1000);
bank.Add(player2, 1000);

bankCasinoFacade.BuyChips(player1, 500);
bankCasinoFacade.BuyChips(player2, 500);

var game = new BlackjackGame(dealer, casino);

game.Join(player1);
game.Join(player2);

game.PlayRound();

using OOP_ICT.Second;

var player = new Player();
var bank  = new Bank();
var casino = new BlackjackCasino();

bank.CreateAccount(player);
casino.RegisterPlayer(player);

bank.Add(player, 100);
casino.AddBlackjack(player, 100);

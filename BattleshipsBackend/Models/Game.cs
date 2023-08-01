namespace BattleshipsBackend.Models
{
    public class Game
    {
        public Guid Id { get; private set; }
        public Player Player1 { get; private set; }
        public Player? Player2 { get; set; }
        public Player CurrentPlayer { get; set; }

        public Game(Player player1)
        {
            Id = Guid.NewGuid();
            Player1 = player1;
            CurrentPlayer = player1;
        }

        public bool TakeShot(Player targetPlayer, Location location)
        {
            return targetPlayer.Board.TakeShot(location);
        }

        public Player? PlayTurn(Guid playerId, Location target)
        {
            if (CurrentPlayer.Id != playerId)
            {
                throw new InvalidOperationException("It's not your turn");
            }

            if (Player2 == null)
            {
                throw new InvalidOperationException("Player 2 needs to join");
            }

            bool shotResult = CurrentPlayer == Player1
                             ? TakeShot(Player2, target)
                             : TakeShot(Player1, target);

            if (shotResult)
            {
                CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;
            }

            return CheckWinCondition();
        }

        public Player? CheckWinCondition()
        {
            if (Player1.CheckAllShipsSunk())
            {
                return Player2;
            }
            else if (Player2.CheckAllShipsSunk())
            {
                return Player1;
            }

            return null;
        }
    }
}

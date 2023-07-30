namespace BattleshipsBackend.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }
        public Player CurrentPlayer { get; set; }

        public Game(Player player1)
        {
            Id = Guid.NewGuid();
            Player1 = player1;
            CurrentPlayer = player1;
        }

        public bool PlayTurn(Location target)
        {
            if (Player2 == null)
            {
                throw new InvalidOperationException("Player 2 needs to join");
            }

            bool shotResult = CurrentPlayer == Player1
                             ? TakeShot(Player2, target)
                             : TakeShot(Player1, target);

            CurrentPlayer = CurrentPlayer == Player1 ? Player2 : Player1;

            CheckWinCondition();

            return shotResult;
        }

        public bool TakeShot(Player targetPlayer, Location target)
        {
            if (CurrentPlayer == null)
            {
                throw new InvalidOperationException("Error: no player");
            }

            bool shotResult = targetPlayer.Board.TakeShot(target);

            return shotResult;
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

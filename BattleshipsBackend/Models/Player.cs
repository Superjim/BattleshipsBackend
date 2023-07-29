namespace BattleshipsBackend.Models
{
	public class Player
	{
		public string Name { get; set; }
		public Guid Id { get; set; }

		private Board Board { get; set; }
		//private List<Ship> Ships { get; set; };

		public Player(string name, Guid id, int boardSize)
		{
			Name = name;
			Id = id;
			Board = new Board(boardSize);
			//Ships = new List<Ship>();
		}
	}
}

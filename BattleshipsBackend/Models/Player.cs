﻿namespace BattleshipsBackend.Models
{
	public class Player
	{
		public string Name { get; set; }
		public Guid Id { get; set; }

		public Board Board { get; set; }
		public List<Ship> Ships { get; set; }

		public Player(string name, Guid id, int boardSize)
		{
			Name = name;
			Id = id;
			Board = new Board(boardSize);
			Ships = new List<Ship>();
		}

		public void AddShip(Ship ship)
		{
			Ships.Add(ship);
		}

		// add ships at random locations
		public void PlaceShips()
		{
			Random rnd = new Random();
			foreach (var ship in Ships)
			{
				bool shipPlaced = false;
				while (!shipPlaced)
				{
					int row = rnd.Next(0, Board.Size);
					int column = rnd.Next(0, Board.Size);

					shipPlaced = Board.PlaceShip(ship, new Location(row, column));
				}
			}
		}

		private Location GetRandomLocation()
		{
			var random = new Random();
			return new Location(random.Next(0, Board.Size), random.Next(0, Board.Size));
		}
	}
}
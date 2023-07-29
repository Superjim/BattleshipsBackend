using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;
using BattleshipsBackend.Models;

namespace BattleshipsUnitTests.ModelTests
{
	public class PlayerTests
	{
		private readonly ITestOutputHelper _output;

		public PlayerTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void PlayerCreatedWithNameId()
		{
			//Arrange
			var name = "Jim";
			var id = Guid.NewGuid();
			var boardSize = 10;

			//Act
			var player = new Player(name, id, boardSize);

			//Assert
			Assert.Equal(name, player.Name);
			Assert.Equal(id, player.Id);
		}

		[Fact]
		public void AllShipsPlaced()
		{
			// Arrange
			var player = new Player("Jim", Guid.NewGuid(), 10);
			var random = new Random();

			var ships = new List<Ship>
	{
		new Ship("Patrol Boat", 2, random.Next(2) == 0),
		new Ship("Cruiser", 3, random.Next(2) == 0),
		new Ship("Submarine", 3, random.Next(2) == 0),
		new Ship("Battleship", 4, random.Next(2) == 0),
		new Ship("Aircraft Carrier", 5, random.Next(2) == 0),
	};

			foreach (var ship in ships)
			{
				player.AddShip(ship);
			}

			// Act
			player.PlaceShips();

			// print gamestate
			var boardRepresentation = player.Board.PrintGrid();
			_output.WriteLine(boardRepresentation);

			// Assert
			foreach (var ship in ships)
			{
				bool shipPlaced = false;
				for (int i = 0; i < player.Board.Size; i++)
				{
					for (int j = 0; j < player.Board.Size; j++)
					{
						if (player.Board.Squares[i, j].Ship == ship)
						{
							shipPlaced = true;
							break;
						}
					}

					if (shipPlaced)
					{
						break;
					}
				}

				Assert.True(shipPlaced);
			}
		}

	}
}

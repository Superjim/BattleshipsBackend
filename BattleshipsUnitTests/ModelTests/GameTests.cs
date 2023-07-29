using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using BattleshipsBackend.Models;
using Xunit.Abstractions;

namespace BattleshipsUnitTests.ModelTests
{
	public class GameTests
	{
		private readonly ITestOutputHelper _output;

		public GameTests(ITestOutputHelper output)
		{
			_output = output;
		}

		[Fact]
		public void PlayGame()
		{
			// Arrange
			var player1 = new Player("Jim", Guid.NewGuid(), 10);
			var player2 = new Player("Joe", Guid.NewGuid(), 10);
			var game = new Game(player1, player2);

			var ships = new List<Ship>
	{
		new Ship("Submarine", 3, true),
		new Ship("Battleship", 4, true),
		new Ship("Carrier", 5, true),
	};

			foreach (var ship in ships)
			{
				player1.AddShip(new Ship(ship.Name, ship.Size, ship.IsVertical));
				player2.AddShip(new Ship(ship.Name, ship.Size, !ship.IsVertical));
			}

			player1.PlaceShips();
			player2.PlaceShips();

			// Act
			StringBuilder output = new StringBuilder();
			while (game.CheckWinCondition() == null)
			{
				var targetRow = new Random().Next(0, 10);
				var targetColumn = new Random().Next(0, 10);
				var target = new Location(targetRow, targetColumn);

				bool shotResult = game.PlayTurn(target);

				string playerName = game.CurrentPlayer == player1 ? player1.Name : player2.Name;
				string result = shotResult ? "Hit" : "Miss";
				output.AppendLine($"{playerName} shoots at ({targetRow}, {targetColumn}). {result}");

				// Switch the current player for the next turn
				game.CurrentPlayer = game.CurrentPlayer == player1 ? player2 : player1;
			}

			// Assert
			Player? winner = game.CheckWinCondition();
			Assert.NotNull(winner);
			output.AppendLine($"Winner: {winner.Name}");

			// Print the output using ITestOutputHelper
			_output.WriteLine(output.ToString());
		}


	}
}

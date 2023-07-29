using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BattleshipsBackend.Models;

namespace BattleshipsUnitTests.ModelTests
{
	public class PlayerTests
	{
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

		//public void PlaceShips()
		//{
		//	//Arrange
		//	var player = new Player("Jim", Guid.NewGuid(), 10);
		//	var ship = new Ship("Submarine", 3);
		//	var location = new Location(0, 0);
		//	var orientation = Orientation.Vertical;

		//	//Act
		//	player.PlaceShip(ship, location, orientation);

		//	//Assert
		//	//ship placed correctly
		//}
	}
}
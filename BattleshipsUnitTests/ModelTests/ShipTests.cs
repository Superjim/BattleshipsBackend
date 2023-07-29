using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BattleshipsBackend.Models;

namespace BattleshipsUnitTests.ModelTests
{
	public class ShipTests
	{
		[Fact]
		public void CreateNewShip()
		{
			//Arrange
			var name = "Carrier";
			var size = 5;

			//Act
			var ship = new Ship(name, size, true);

			//Assert
			Assert.Equal(name, ship.Name);
			Assert.Equal(size, ship.Size);
		}

		[Fact]
		public void ShipAddHit()
		{
			//Arrange
			var ship = new Ship("Submarine", 3, true);

			//Act
			ship.AddHit();
			ship.AddHit();

			//Assert
			Assert.Equal(2, ship.Hits);
		}

		[Fact]
		public void CheckShipSunkFalse()
		{
			//Arrange
			var ship = new Ship("Submarine", 3, true);

			//Act
			ship.AddHit();

			//Assert
			Assert.False(ship.IsShipSunk());
		}

		[Fact]
		public void CheckShipSunkTrue()
		{
			//Arrange
			var ship = new Ship("Submarine", 3, true);

			//Act
			ship.AddHit();
			ship.AddHit();
			ship.AddHit();

			//Assert
			Assert.True(ship.IsShipSunk());
		}
	}
}

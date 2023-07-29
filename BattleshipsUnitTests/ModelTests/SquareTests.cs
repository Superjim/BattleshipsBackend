using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BattleshipsBackend.Models;
using Xunit;

namespace BattleshipsUnitTests.ModelTests
{
	public class SquareTests
	{
		[Fact]
		public void ShootEmptySquare()
		{
			// Arrange
			var square = new Square();

			// Act
			square.MarkShotAndHitShip();

			// Assert
			Assert.True(square.HasShotBeenTaken);
			Assert.Null(square.Ship);
		}

		[Fact]
		public void ShootSquareWithShip()
		{
			// Arrange
			var square = new Square();
			square.Ship = new Ship("Battleship", 4, true);

			// Act
			var result = square.MarkShotAndHitShip();

			// Assert
			Assert.True(result);
			Assert.True(square.HasShotBeenTaken);
			Assert.NotNull(square.Ship);
		}

		[Fact]
		public void CantShootTheSameSquareTwice()
		{
			// Arrange
			var square = new Square();
			square.MarkShotAndHitShip();

			// Act
			var result = square.MarkShotAndHitShip();

			// Assert
			Assert.False(result);
			Assert.True(square.HasShotBeenTaken);
		}
	}
}
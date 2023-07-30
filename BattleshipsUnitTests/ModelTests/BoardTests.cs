using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BattleshipsBackend.Models;

namespace BattleshipsUnitTests.ModelTests
{
    public class BoardTests
    {
        [Fact]
        public void CheckShipPlacedBoardEmpty()
        {
            //Arrange
            var board = new Board(10);
            var ship = new Ship("Submarine", 3, true);
            var location = new Location(1, 1);

            //Act
            board.PlaceShip(ship, location);

            //Assert
            Assert.Equal(ship, board.Squares[location.Row][location.Column].Ship);

        }

        [Fact]
        public void CheckShipWontPlaceIfOverlapping()
        {
            //Arrange
            var board = new Board(10);
            var carrier = new Ship("Carrier", 5, true);
            var submarine = new Ship("Submarine", 3, true);
            var locationCarrier = new Location(1, 1);
            var locationSub = new Location(1, 1);

            board.PlaceShip(carrier, locationCarrier);

            //Act
            var test = board.PlaceShip(submarine, locationSub);

            //Asssert
            Assert.False(test);
        }

        [Fact]
        public void CheckShipWontPlaceOutOfBoundsVertical()
        {
            //Arrange
            var board = new Board(10);
            var carrier = new Ship("Carrier", 5, true);
            var locationCarrier = new Location(1, 9);

            //Act
            var test = board.PlaceShip(carrier, locationCarrier);

            //Assert
            Assert.False(test);
        }

        [Fact]
        public void CheckShipWontPlaceOutOfBoundsHorizontal()
        {
            //Arrange
            var board = new Board(10);
            var carrier = new Ship("Carrier", 5, false);
            var locationCarrier = new Location(9, 1);

            //Act
            var test = board.PlaceShip(carrier, locationCarrier);

            //Assert
            Assert.False(test);
        }

        [Fact]
        public void TakeShotAndMiss()
        {
            // Arrange
            var board = new Board(10);
            var location = new Location(5, 5);

            // Act
            var test = board.TakeShot(location);

            // Assert
            Assert.False(test);
            Assert.True(board.Squares[location.Row][location.Column].HasShotBeenTaken);
        }

        [Fact]
        public void TakeShotAndHit()
        {
            // Arrange
            var board = new Board(10);
            var ship = new Ship("Submarine", 3, true);
            var location = new Location(3, 3);

            // Act
            bool shipPlaced = board.PlaceShip(ship, location);
            var test = board.TakeShot(location);

            // Assert
            Assert.True(shipPlaced, "ship not placed");
            Assert.True(test, "shot missed the ship");
            Assert.True(board.Squares[location.Row][location.Column].HasShotBeenTaken);
            Assert.True(board.Squares[location.Row][location.Column].IsShipHitResult());
        }

        [Fact]
        public void TakeShotAndSinkShip()
        {
            // Arrange
            var board = new Board(10);
            var ship = new Ship("Battleship", 4, false);
            var location1 = new Location(3, 2);
            var location2 = new Location(3, 3);
            var location3 = new Location(3, 4);
            var location4 = new Location(3, 5);

            bool shipPlaced = board.PlaceShip(ship, location1);

            // Act
            board.TakeShot(location1);
            board.TakeShot(location2);
            board.TakeShot(location3);
            board.TakeShot(location4);

            // assert
            Assert.True(shipPlaced, "ship not placed");
            string gameState = board.PrintGrid();
            Assert.True(ship.IsShipSunk(), gameState);
        }

        [Fact]
        public void TakeInvalidShot()
        {
            // Arrange
            var board = new Board(10);
            var location = new Location(1, 15); // out of bounds

            // Act
            var test = board.TakeShot(location);

            // Assert
            Assert.False(test);
        }
    }
}

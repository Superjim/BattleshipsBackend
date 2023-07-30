using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using BattleshipsBackend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BattleshipsUnitTests.ControllerTests
{
    public class PlayerControllerTests
    {
        [Fact]
        public void PostPlayerReturns200()
        {
            // Arrange
            var controller = new PlayerController();
            string name = "Jim";

            // Act
            var result = controller.Post(name);

            // Assetr
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult.Value);
            Assert.IsType<Guid>(okResult.Value);
        }

        [Fact]
        public void PostPlayerEnptyNameReturnsErr()
        {
            // Arrange
            var controller = new PlayerController();
            string name = string.Empty;

            // Act
            var result = controller.Post(name);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Please enter a name", badRequestResult.Value);
        }

        [Fact]
        public void PostPlayerNullNameReturnsErr()
        {
            // Arrange
            var controller = new PlayerController();
            string name = null;

            // Act
            var result = controller.Post(name);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.Equal("Please enter a name", badRequestResult.Value);
        }
    }
}

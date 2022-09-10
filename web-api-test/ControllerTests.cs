using GameAPI_MSA2022.Controllers;
using Service_Layer.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using MediatR;
using FluentAssertions;

namespace web_api_test
{

    public class ControllerTests
    {


        [Fact]
        public async Task GetAll_ReturnsListofGames()
        {
            // Arrange
            var _mockMediator = new Mock<IMediator>();
            var _mockLogger = new Mock<ILogger<MediatRController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var _mediatRController = new MediatRController(_mockMediator.Object, _mockLogger.Object, _mockDistributedCache.Object);

            // Act
            var result = await _mediatRController.GetAll();

            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void PostGame_ReturnCreatedAtActionOkResponse()
        {
            // Arrange
            var _mockLogger = new Mock<ILogger<GameController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var controller = new GameController(_mockLogger.Object, _mockDistributedCache.Object);

            // Act
            var result = controller.Create(new Game { Name = "TestingGame", IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }
        [Fact]
        public void PostBlankGame_ReturnCreatedAtActionErrorResponse()
        {
            // Arrange
            var _mockLogger = new Mock<ILogger<GameController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var controller = new GameController(_mockLogger.Object, _mockDistributedCache.Object);
            // Act
            var result = controller.Create(new Game { IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }
        [Fact]
        public void PutGameWithNonMatchingId_ResultsInBadRequestError()
        {
            // Arrange
            var _mockLogger = new Mock<ILogger<GameController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var controller = new GameController(_mockLogger.Object, _mockDistributedCache.Object);
            // Act
            var result = controller.Update(2, new Game { Id = 3, Name = "New Game", IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }


        [Fact]
        public void PutGameWithInvalidId_ResultsInNotFoundError()
        {
            // Arrange
            var _mockLogger = new Mock<ILogger<GameController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var controller = new GameController(_mockLogger.Object, _mockDistributedCache.Object);
            // Act
            var result = controller.Update(10, new Game { Id = 10, Name = "New Game", IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteGame_ReturnsNoContent()
        {
            // Arrange
            var _mockLogger = new Mock<ILogger<GameController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var controller = new GameController(_mockLogger.Object, _mockDistributedCache.Object);
            // Act
            var result = controller.Delete(2);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public void DeleteGame_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var _mockLogger = new Mock<ILogger<GameController>>();
            var _mockDistributedCache = new Mock<IDistributedCache>();
            var controller = new GameController(_mockLogger.Object, _mockDistributedCache.Object);

            // Act
            var result = controller.Delete(7);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

    }
}

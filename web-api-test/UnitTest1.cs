using GameAPI_MSA2022.Controllers;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;
using NSubstitute;

namespace web_api_test
{
    public class UnitTest1
    {
        [Fact]
        public void GetAll_ReturnsListofEpicGames()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.GetAll();

            // Assert
            result.Value.Should().BeOfType<List<Game>>();
        }


        [Fact]
        public void GetGameByInvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.Get(6);

            // Assert
            result.Result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void PostGame_ReturnCreatedAtActionOkResponse()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.Create(new Game { Name = "TestingGame", IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }
        [Fact]
        public void PostBlankGame_ReturnCreatedAtActionErrorResponse()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.Create(new Game { IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<CreatedAtActionResult>();
        }
        [Fact]
        public void PutGameWithNonMatchingId_ResultsInBadRequestError()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.Update(2, new Game { Id = 3, Name = "New Game", IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }


        [Fact]
        public void PutGameWithInvalidId_ResultsInNotFoundError()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.Update(10, new Game { Id = 10, Name = "New Game", IsFree = false, Genre = "" });

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteGame_ReturnsNoContent()
        {
            // Arrange
            var controller = new GameController();
            // Act
            var result = controller.Delete(2);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
        [Fact]
        public void DeleteGame_InvalidId_ReturnsNotFound()
        {
            // Arrange
            var controller = new GameController();

            // Act
            var result = controller.Delete(7);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}

using GameAPI_MSA2022.Controllers;
using Service_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;
using NSubstitute;

namespace web_api_test
{
    public class UnitTest2
    {
        [Fact]
        public void GetAll_ReturnsListofEpicGames()
        {
            var gameController = Substitute.For<GameController>();

            var games = GamesService.GetAll();

            gameController.GetAll().Returns(games);
        }
    }
}

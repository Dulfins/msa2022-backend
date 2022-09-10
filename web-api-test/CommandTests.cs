using GameAPI_MSA2022.Controllers;
using Service_Layer.Services;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using System.Net;
using NSubstitute;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using Moq;
using MediatR;
using Domain_Layer.Data;
using GameAPI_MSA2022.CQRS.Commands;

namespace web_api_test
{
    public class CommandTests
    {
        //[Fact]
        //public async Task AddGame_Command()
        //{
        //    var context = new Mock<GameContext>();
        //    var games = new List<Game>();

        //    var handler = new AddGameCommand.AddGameCommandHandler(context.Object);

        //    var command = new AddGameCommand { Name = "Hello", IsFree = false, Genre = "Adventure" };



        //}

    }
}

using Microsoft.AspNetCore.Mvc;
using Domain_Layer.Models;
using Service_Layer.Services;
using System.Text.Json;

namespace GameAPI_MSA2022.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheapSharkController : ControllerBase
{

    private readonly HttpClient _client;
    /// <summary />
    public CheapSharkController(IHttpClientFactory clientFactory)
    {
        if (clientFactory is null)
        {
            throw new ArgumentNullException(nameof(clientFactory));
        }
        _client = clientFactory.CreateClient("cheapshark");
    }
    /// <summary>
    /// Gets the raw JSON for games that contains that name and converts to a deserialized json
    /// </summary>
    /// <returns>A deserialized JSON list representing games containing that name and their cheapest price</returns>
    [HttpGet("{title}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(string title)
    { 
        var res = await _client.GetAsync($"?title={title}");
        var content = await res.Content.ReadAsStringAsync();

        var results = JsonSerializer.Deserialize<IList<GameDetails>>(content);
        if (results is null) { return NotFound(); }
        return Ok(results);
    }

    ///// <summary>
    ///// Gets deserialized json from one of the games from EpicGames List
    ///// </summary>
    ///// <remarks>
    ///// Retrieves Data from other endpoint and uses that to search for its lowest price
    ///// </remarks>
    ///// <returns>A deserialized JSON list representing games containing the game's name and their cheapest price</returns>
  
    //[HttpGet("game/{id}")]
    //[ProducesResponseType(200)]
    //[ProducesResponseType(404)]
    //public async Task<IActionResult> GetFromEpicGames(int id)
    //{
    //    var game = GamesService.Get(id);
    //    if (game is null)
    //        return NotFound();
    //    var title = game.Name;
    //    var res = await _client.GetAsync($"?title={title}");
    //    var content = await res.Content.ReadAsStringAsync();

    //    var results = JsonSerializer.Deserialize<IList<GameDetails>>(content);
    //    if (results is null) {return NotFound();}
    //    return Ok(results);
    //}

}


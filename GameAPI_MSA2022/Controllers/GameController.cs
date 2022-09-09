using Domain_Layer.Models;
using MediatR;
using Service_Layer.Services;
using Microsoft.AspNetCore.Mvc;


namespace GameAPI_MSA2022.Controllers;

[ApiController]
[Route("[controller]")]
public class GameController : ControllerBase
{
    private IMediator _mediator;

    public GameController(IMediator mediator)
    {
        _mediator = mediator;
    }


    /// <summary>
    /// This endpoint takes no arguments and gives you a list of games, 
    /// </summary>
    /// <returns>A list of stored games</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    public ActionResult<List<Game>> GetAll() => GamesService.GetAll();


    /// <summary>
    /// Get the game entry with the requested id
    /// </summary>
    /// <param name="id">Id must exist in list</param>
    /// <returns>Returns game with the requested id</returns>
    /// <response code="200">Successfully returns entry with requested id</response>
    /// <response code="404">No entry with that id exist</response>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    public ActionResult<Game> Get(int id)
    {
        var game = GamesService.Get(id);
        if (game == null)
            return NotFound();

        return game;
    }
    /// <summary>
    /// Add a new game entry to Games list
    /// </summary>
    /// <param name="game">Id field not required but name field is required</param>
    /// <returns>A new list updated with the new entry</returns>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     POST /Game/{id}
    ///     {
    ///         "name": "Awesome Game",
    ///         "isFree": true,
    ///         "Genre": "Adventure"
    ///     }
    ///     
    /// Don't need to include id in body
    /// </remarks>
    /// <response code="201">Entry successfully added</response>
    /// <response code="400">Bad Request, Name field is required</response>
    [HttpPost]
    public IActionResult Create(Game game)
    {
        GamesService.Add(game);
        return CreatedAtAction(nameof(Create), new { id = game.Id }, game);
    }
    /// <summary>
    /// Updates an existing entry
    /// </summary>
    /// <param name="id">Id must exist in list</param>
    /// <param name="game">id in body must be same as parameter id above</param>
    /// <returns>A list with the updated entry</returns>
    /// <remarks>
    /// Sample Request:
    /// 
    ///     PUT /Game/{id}
    ///     {
    ///         "id": 2
    ///         "name": "Awesome Game",
    ///         "isFree": true,
    ///         "Genre": "Adventure"
    ///     }
    ///     
    /// MAKE SURE ID IS SAME IN BODY AND IN REQUEST!
    /// 
    /// </remarks>
    /// <response code="204">Entry successfully updated</response>
    /// <response code="404">Entry not found</response>
    /// <response code="400">Bad Request, Id in request body needs to match parameter id. Name field is required</response>
    [HttpPut("{id}")]
    public IActionResult Update(int id, Game game)
    {
        if (id != game.Id)
            return BadRequest();

        var existingGame = GamesService.Get(id);
        if (existingGame is null)
            return NotFound();

        GamesService.Update(game);

        return NoContent();
    }

    /// <summary>
    /// Deletes the entry with the corrosponding id
    /// </summary>
    /// <param name="id">Id must exist in list</param>
    /// <returns>Updated list with entry with requested id removed</returns>
    /// <response code="204">Entry successfully removed</response>
    /// <response code="404">Entry not found</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var game = GamesService.Get(id);

        if (game is null)
            return NotFound();

        GamesService.Delete(id);

        return NoContent();
    }
}
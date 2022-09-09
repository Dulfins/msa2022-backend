using Domain_Layer.Models;
using MediatR;
namespace Service_Layer.Services;

public class GamesService : IRequest<Game>
{
    static List<Game> Games { get; } 
    static int nextId = 5;
    static GamesService()
    {
        Games = new List<Game>
        {
            new Game {Id = 1, Name = "Minecraft", IsFree = false, Genre = "Sandbox"},
            new Game {Id = 2, Name = "The Last of Us", IsFree = false, Genre = "Survival Horror"},
            new Game {Id = 3, Name = "Valorant", IsFree = true, Genre = "FPS Shooter"},
            new Game {Id = 4, Name = "Portal 2", IsFree = false, Genre = "Puzzle"},
        };
    }

    public static List<Game> GetAll() => Games;


    public static Game? Get(int Id) => Games.FirstOrDefault(p => p.Id == Id);

    public static void Add(Game game)
    {
        game.Id = nextId++;
        Games.Add(game);
    }

    public static void Delete(int id)
    {
        var game = Get(id);
        if (game is null){return;}

        Games.Remove(game);
    }

    public static void Update(Game game)
    {
        var index = Games.FindIndex(p => p.Id == game.Id);
        if (index == -1){return;}
        Games[index] = game;
    }
 
}
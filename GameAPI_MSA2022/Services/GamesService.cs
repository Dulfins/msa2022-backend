using GameAPI_MSA2022.Models;

namespace GameAPI_MSA2022.Services;

public static class GamesService
{

    static List<EpicGame> Games { get; }
    static int nextId = 5;
    static GamesService()
    {
        Games = new List<EpicGame>
        {
            new EpicGame {Id = 1, Name = "Minecraft", IsFree = false, Genre = "Sandbox"},
            new EpicGame {Id = 2, Name = "The Last of Us", IsFree = false, Genre = "Survival Horror"},
            new EpicGame {Id = 3, Name = "Valorant", IsFree = true, Genre = "FPS Shooter"},
            new EpicGame {Id = 4, Name = "Portal 2", IsFree = false, Genre = "Puzzle"},
        };
    }

    public static List<EpicGame> GetAll() => Games;

    public static EpicGame? Get(int Id) => Games.FirstOrDefault(p => p.Id == Id);

    public static void Add(EpicGame game)
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

    public static void Update(EpicGame game)
    {
        var index = Games.FindIndex(p => p.Id == game.Id);
        if (index == -1){return;}
        Games[index] = game;
    }
 
}
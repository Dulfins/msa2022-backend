using Domain_Layer.Data;
using MediatR;
using Domain_Layer.Models;
namespace GameAPI_MSA2022.CQRS.Commands
{
    public class AddGameCommand : IRequest<int>
    {
        public string Name { get; set; }
        public bool IsFree { get; set; }
        public string Genre { get; set; }

        public class AddGameCommandHandler : IRequestHandler<AddGameCommand, int>
        {
            private GameContext context;
            public AddGameCommandHandler(GameContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(AddGameCommand command, CancellationToken cancellationToken)
            {
                var game = new Game();
                game.Name = command.Name;
                game.IsFree = command.IsFree;
                game.Genre = command.Genre;

                context.Game.Add(game);
                await context.SaveChangesAsync();
                return game.Id;
            }
        }
    }
}

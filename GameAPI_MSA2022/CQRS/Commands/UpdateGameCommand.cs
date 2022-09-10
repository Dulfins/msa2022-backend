using MediatR;
using Domain_Layer.Models;
using Domain_Layer.Data;

namespace GameAPI_MSA2022.CQRS.Commands
{
    public class UpdateGameCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsFree { get; set; }
        public string Genre { get; set; }

        public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, int>
        {
            private GameContext context;
            public UpdateGameCommandHandler(GameContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(UpdateGameCommand command, CancellationToken cancellationToken)
            {
                var game = context.Game.Where(a => a.Id == command.Id).FirstOrDefault();

                if (game == null)
                {
                    return default;
                }
                else
                {
                    game.Name = command.Name;
                    game.IsFree = command.IsFree;
                    game.Genre = command.Genre;
                    await context.SaveChangesAsync();
                    return game.Id;
                }
            }
        }
    }
}

using Domain_Layer.Data;
using MediatR;
using Domain_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace GameAPI_MSA2022.CQRS.Commands
{
    public class DeleteGameByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
        public class DeleteGameByIdCommandHandler : IRequestHandler<DeleteGameByIdCommand, int>
        {
            private GameContext context;
            public DeleteGameByIdCommandHandler(GameContext context)
            {
                this.context = context;
            }
            public async Task<int> Handle(DeleteGameByIdCommand command, CancellationToken cancellationToken)
            {
                var product = await context.Game.Where(a => a.Id == command.Id).FirstOrDefaultAsync();
                context.Game.Remove(product);
                await context.SaveChangesAsync();
                return product.Id;
            }
        }
    }
}

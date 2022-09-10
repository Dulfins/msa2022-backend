using Domain_Layer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain_Layer.Data;

namespace GameAPI_MSA2022.CQRS.Queries
{
    public class GetGameByIdQuery : IRequest<Game>
    {
        public int Id { get; set; }
        public class GetGameByIdQueryHandler : IRequestHandler<GetGameByIdQuery, Game>
        {
            private GameContext context;
            public GetGameByIdQueryHandler(GameContext context)
            {
                this.context = context;
            }
            public async Task<Game> Handle(GetGameByIdQuery query, CancellationToken cancellationToken)
            {
                var product = await context.Game.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
                return product;
            }
        }
    }
}

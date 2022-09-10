using Domain_Layer.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain_Layer.Data;

namespace GameAPI_MSA2022.CQRS.Queries
{
    public class GetAllGameQuery : IRequest<IEnumerable<Game>>
    {
        public class GetAllGameQueryHandler : IRequestHandler<GetAllGameQuery, IEnumerable<Game>>
        {
            private GameContext context;
            public GetAllGameQueryHandler(GameContext context)
            {
                this.context = context;
            }
            public async Task<IEnumerable<Game>> Handle(GetAllGameQuery query, CancellationToken cancellationToken)
            {
                var gameList = await context.Game.ToListAsync();
                return gameList;
            }
        }
    }
}

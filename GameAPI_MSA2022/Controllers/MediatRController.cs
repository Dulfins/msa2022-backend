using Microsoft.AspNetCore.Mvc;
using MediatR;
using GameAPI_MSA2022.CQRS.Commands;
using GameAPI_MSA2022.CQRS.Queries;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using StackExchange.Redis;
using Service_Layer.Services;
using Domain_Layer.Models;

namespace GameAPI_MSA2022.Controllers
{
    [Route("mediatr/[controller]")]
    [ApiController]
    public class MediatRController : ControllerBase
    {

        private IMediator mediator;
        private ILogger<MediatRController> logger;
        private IDistributedCache distributedCache;

       

        public MediatRController(IMediator mediator, ILogger<MediatRController> logger, IDistributedCache distributedCache)
        {
            this.mediator = mediator;
            this.logger = logger;
            this.distributedCache = distributedCache;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddGameCommand command)
        {
            return Ok(await mediator.Send(command));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var cacheKey = "mediatRGameList";
            string serializedGameList;
            List<Game> gameList = new ();
            var redisGameList = await distributedCache.GetAsync(cacheKey);
            if (redisGameList != null)
            {
                serializedGameList = Encoding.UTF8.GetString(redisGameList);
                gameList = JsonConvert.DeserializeObject<List<Game>>(serializedGameList);
                //gameList = (List<Game>)await mediator.Send(new GetAllGameQuery());
            }
            else
            {
                gameList = (List<Game>) await mediator.Send(new GetAllGameQuery());
                serializedGameList = JsonConvert.SerializeObject(gameList);
                redisGameList = Encoding.UTF8.GetBytes(serializedGameList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisGameList, options);
            }

            return Ok(gameList);

            // old code
            //return Ok(await mediator.Send(new GetAllGameQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await mediator.Send(new GetGameByIdQuery { Id = id }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateGameCommand command)
        {
            command.Id = id;
            return Ok(await mediator.Send(command));
        }

        [HttpDelete("{id}")]    
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await mediator.Send(new DeleteGameByIdCommand { Id = id }));
        }
    }
}

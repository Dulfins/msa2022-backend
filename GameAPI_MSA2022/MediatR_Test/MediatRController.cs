using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace GameAPI_MSA2022.MediatR_Test
{

    // Throws an error when ran idk

    [ApiController]
    [Route("[controller]")]
    public class MediatRController : ControllerBase
    {
        private IMediator _mediator;

        public MediatRController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("SendRequest")]
        public async Task<string> SendRequest(string name)
        {
            var result = await _mediator.Send(new MyRequest(name));
            return result;
        }
    }
}

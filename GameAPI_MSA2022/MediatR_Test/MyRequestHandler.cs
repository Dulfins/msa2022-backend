using MediatR;

namespace GameAPI_MSA2022.MediatR_Test
{
    public class MyRequestHandler : IRequestHandler<MyRequest, string>
    {
        public async Task<string> Handle(MyRequest request, CancellationToken cancellationToken)
        {
            return $"Hello {request.Name}";
        }
    }
}

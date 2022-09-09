using MediatR;
namespace GameAPI_MSA2022.MediatR_Test
{
    public class MyRequest : IRequest<string>
    {
        public string Name { get; }
        public MyRequest(string name)
        {
            Name = name;
        }
    }
}

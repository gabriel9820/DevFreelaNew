using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetUserById;

public class GetUserByIdQuery : IRequest<ResultViewModel<UserViewModel>>
{
    public int Id { get; private set; }

    public GetUserByIdQuery(int id)
    {
        Id = id;
    }
}

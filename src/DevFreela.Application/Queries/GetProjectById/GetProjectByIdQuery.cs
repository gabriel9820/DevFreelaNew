using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetProjectById;

public class GetProjectByIdQuery : IRequest<ResultViewModel<ProjectViewModel>>
{
    public int Id { get; private set; }

    public GetProjectByIdQuery(int id)
    {
        Id = id;
    }
}

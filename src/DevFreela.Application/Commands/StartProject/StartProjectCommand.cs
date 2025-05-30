using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectCommand : IRequest<ResultViewModel>
{
    public int Id { get; private set; }

    public StartProjectCommand(int id)
    {
        Id = id;
    }
}

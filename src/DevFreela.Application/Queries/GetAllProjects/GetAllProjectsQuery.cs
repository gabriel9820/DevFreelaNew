using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Queries.GetAllProjects;

public class GetAllProjectsQuery : IRequest<ResultViewModel<List<ProjectItemViewModel>>>
{
    public string? Search { get; set; } = string.Empty;
    public int Page { get; private set; } = 0;
    public int Size { get; set; } = 3;
}

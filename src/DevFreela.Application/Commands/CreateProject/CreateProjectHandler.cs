using DevFreela.Application.Models;
using DevFreela.Application.Notifications.ProjectCreated;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectHandler : IRequestHandler<CreateProjectCommand, ResultViewModel<int>>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IMediator _mediator;

    public CreateProjectHandler(IProjectRepository projectRepository, IMediator mediator)
    {
        _projectRepository = projectRepository;
        _mediator = mediator;
    }

    public async Task<ResultViewModel<int>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = request.ToEntity();

        var id = await _projectRepository.CreateAsync(project);

        var projectNotification = new ProjectCreatedNotification(id, project.Title, project.TotalCost);
        await _mediator.Publish(projectNotification);

        return ResultViewModel<int>.Success(id);
    }
}

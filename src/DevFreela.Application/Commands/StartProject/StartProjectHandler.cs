using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.StartProject;

public class StartProjectHandler : IRequestHandler<StartProjectCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public StartProjectHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(StartProjectCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.Id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        project.Start();

        await _projectRepository.UpdateAsync();

        return ResultViewModel.Success();
    }
}

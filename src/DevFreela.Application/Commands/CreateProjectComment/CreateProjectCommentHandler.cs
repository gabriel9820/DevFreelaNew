using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment;

public class CreateProjectCommentHandler : IRequestHandler<CreateProjectCommentCommand, ResultViewModel>
{
    private readonly IProjectRepository _projectRepository;

    public CreateProjectCommentHandler(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<ResultViewModel> Handle(CreateProjectCommentCommand request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository.GetByIdAsync(request.ProjectId);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        var comment = new ProjectComment(request.Content, request.ProjectId, request.UserId);

        await _projectRepository.AddCommentAsync(comment);

        return ResultViewModel.Success();
    }
}

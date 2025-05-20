using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.CreateProjectComment;

public class CreateProjectCommentCommand : IRequest<ResultViewModel>
{
    public string Content { get; set; }
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}

using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.PasswordRecoveryRequest;

public class PasswordRecoveryRequestCommand : IRequest<ResultViewModel>
{
    public string Email { get; set; }
}

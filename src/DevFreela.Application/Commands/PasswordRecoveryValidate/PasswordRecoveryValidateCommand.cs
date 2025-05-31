using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.PasswordRecoveryValidate;

public class PasswordRecoveryValidateCommand : IRequest<ResultViewModel>
{
    public string Email { get; set; }
    public string Code { get; set; }
}

using DevFreela.Application.Commands.CreateUser;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateUserValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("E-mail é inválido");

        RuleFor(x => x.BirthDate)
            .Must(x => x < DateTime.Now.AddYears(-18)).WithMessage("Deve ter mais de 18 anos");
    }
}

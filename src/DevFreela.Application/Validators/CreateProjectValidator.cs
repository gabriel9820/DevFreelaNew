using DevFreela.Application.Commands.CreateProject;
using FluentValidation;

namespace DevFreela.Application.Validators;

public class CreateProjectValidator : AbstractValidator<CreateProjectCommand>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Título é obrigatório")
            .MaximumLength(50).WithMessage("Título deve ter no máximo 50 caracteres");

        RuleFor(x => x.TotalCost)
            .GreaterThanOrEqualTo(99).WithMessage("Valor deve ser maior ou igual a 99");
    }
}

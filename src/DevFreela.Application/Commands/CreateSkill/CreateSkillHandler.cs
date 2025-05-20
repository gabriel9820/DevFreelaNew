using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.CreateSkill;

public class CreateSkillHandler : IRequestHandler<CreateSkillCommand, ResultViewModel<int>>
{
    private readonly ISkillRepository _skillRepository;

    public CreateSkillHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<ResultViewModel<int>> Handle(CreateSkillCommand request, CancellationToken cancellationToken)
    {
        var skill = request.ToEntity();

        await _skillRepository.CreateAsync(skill);

        return ResultViewModel<int>.Success(skill.Id);
    }
}

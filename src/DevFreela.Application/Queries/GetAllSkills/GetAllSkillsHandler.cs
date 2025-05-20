using DevFreela.Application.Models;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Queries.GetAllSkills;

public class GetAllSkillsHandler : IRequestHandler<GetAllSkillsQuery, ResultViewModel<List<SkillViewModel>>>
{
    private readonly ISkillRepository _skillRepository;

    public GetAllSkillsHandler(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public async Task<ResultViewModel<List<SkillViewModel>>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
    {
        var skills = await _skillRepository.GetAllAsync();

        var model = skills.Select(SkillViewModel.FromEntity).ToList();

        return ResultViewModel<List<SkillViewModel>>.Success(model);
    }
}

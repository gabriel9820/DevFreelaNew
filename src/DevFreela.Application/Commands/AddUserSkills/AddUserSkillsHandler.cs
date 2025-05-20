using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using MediatR;

namespace DevFreela.Application.Commands.AddUserSkills;

public class AddUserSkillsHandler : IRequestHandler<AddUserSkillsCommand, ResultViewModel>
{
    private readonly IUserRepository _userRepository;

    public AddUserSkillsHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ResultViewModel> Handle(AddUserSkillsCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.ExistsAsync(request.UserId);

        if (!user)
        {
            return ResultViewModel.Error("Usuário não encontrado");
        }

        var skills = request.SkillsIds.Select(x => new UserSkill(request.UserId, x)).ToList();

        await _userRepository.AddSkillsAsync(skills);

        return ResultViewModel.Success();
    }
}

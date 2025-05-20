using DevFreela.Application.Models;
using MediatR;

namespace DevFreela.Application.Commands.AddUserSkills;

public class AddUserSkillsCommand : IRequest<ResultViewModel>
{
    public int UserId { get; private set; }
    public List<int> SkillsIds { get; private set; }

    public AddUserSkillsCommand(int userId, List<int> skillsIds)
    {
        UserId = userId;
        SkillsIds = skillsIds;
    }
}

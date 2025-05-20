using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateSkill;

public class CreateSkillCommand : IRequest<ResultViewModel<int>>
{
    public string Description { get; set; }

    public Skill ToEntity()
    {
        return new Skill(Description);
    }
}

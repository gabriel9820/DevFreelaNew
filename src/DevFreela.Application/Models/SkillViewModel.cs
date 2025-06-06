using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class SkillViewModel
{
    public string Description { get; private set; }

    public SkillViewModel(string description)
    {
        Description = description;
    }

    public static SkillViewModel FromEntity(Skill skill)
    {
        return new SkillViewModel(skill.Description);
    }
}

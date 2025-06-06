using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface ISkillRepository
{
    Task<List<Skill>> GetAllAsync();
    Task<int> CreateAsync(Skill skill);
}

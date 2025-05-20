using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IUserRepository
{
    Task<User?> GetDetailsByIdAsync(int id);
    Task<int> CreateAsync(User user);
    Task AddSkillsAsync(List<UserSkill> skills);
    Task<bool> ExistsAsync(int id);
}

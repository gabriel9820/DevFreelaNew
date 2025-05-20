using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public UserRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddSkillsAsync(List<UserSkill> skills)
    {
        await _dbContext.UsersSkills.AddRangeAsync(skills);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user.Id;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbContext.Users.AnyAsync(x => x.Id == id);
    }

    public async Task<User?> GetDetailsByIdAsync(int id)
    {
        return await _dbContext.Users
            .Include(x => x.Skills)
            .ThenInclude(x => x.Skill)
            .SingleOrDefaultAsync(x => x.Id == id);
    }
}

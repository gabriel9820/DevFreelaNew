using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class SkillRepository : ISkillRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public SkillRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> CreateAsync(Skill skill)
    {
        await _dbContext.Skills.AddAsync(skill);
        await _dbContext.SaveChangesAsync();

        return skill.Id;
    }

    public async Task<List<Skill>> GetAllAsync()
    {
        return await _dbContext.Skills
            .Where(x => !x.IsDeleted)
            .ToListAsync();
    }
}

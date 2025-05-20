using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Infrastructure.Persistence.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectRepository(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddCommentAsync(ProjectComment comment)
    {
        await _dbContext.ProjectsComments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<int> CreateAsync(Project project)
    {
        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return project.Id;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbContext.Projects.AnyAsync(x => x.Id == id);
    }

    public async Task<List<Project>> GetAllAsync(string search, int page, int size)
    {
        return await _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted && (search == null || x.Title.Contains(search) || x.Description.Contains(search)))
            .Skip(page * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(int id)
    {
        return await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Project?> GetDetailsByIdAsync(int id)
    {
        return await _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task UpdateAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}

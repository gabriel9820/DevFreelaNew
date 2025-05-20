using DevFreela.Core.Entities;

namespace DevFreela.Core.Repositories;

public interface IProjectRepository
{
    Task<List<Project>> GetAllAsync(string search, int page, int size);
    Task<Project?> GetByIdAsync(int id);
    Task<Project?> GetDetailsByIdAsync(int id);
    Task<int> CreateAsync(Project project);
    Task UpdateAsync();
    Task AddCommentAsync(ProjectComment comment);
    Task<bool> ExistsAsync(int id);
}

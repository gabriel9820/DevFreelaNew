using DevFreela.Application.Interfaces;
using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using DevFreela.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevFreela.Application.Services;

public class ProjectService : IProjectService
{
    private readonly DevFreelaDbContext _dbContext;

    public ProjectService(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ResultViewModel> Complete(int id)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        project.Complete();

        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel<int>> Create(CreateProjectInputModel model)
    {
        var project = model.ToEntity();

        await _dbContext.Projects.AddAsync(project);
        await _dbContext.SaveChangesAsync();

        return ResultViewModel<int>.Success(project.Id);
    }

    public async Task<ResultViewModel> CreateComment(int id, CreateProjectCommentInputModel model)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        var comment = new ProjectComment(model.Content, id, model.UserId);

        await _dbContext.ProjectsComments.AddAsync(comment);
        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> Delete(int id)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        project.SetAsDeleted();

        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel<List<ProjectItemViewModel>>> GetAll(string search, int page, int size)
    {
        var projects = await _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Where(x => !x.IsDeleted && (search == null || x.Title.Contains(search) || x.Description.Contains(search)))
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        var model = projects.Select(ProjectItemViewModel.FromEntity).ToList();

        return ResultViewModel<List<ProjectItemViewModel>>.Success(model);
    }

    public async Task<ResultViewModel<ProjectViewModel>> GetById(int id)
    {
        var project = await _dbContext.Projects
            .Include(x => x.Client)
            .Include(x => x.Freelancer)
            .Include(x => x.Comments)
            .SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            return ResultViewModel<ProjectViewModel>.Error("Projeto não encontrado");
        }

        var model = ProjectViewModel.FromEntity(project);

        return ResultViewModel<ProjectViewModel>.Success(model);
    }

    public async Task<ResultViewModel> Start(int id)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        project.Start();

        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }

    public async Task<ResultViewModel> Update(int id, UpdateProjectInputModel model)
    {
        var project = await _dbContext.Projects.SingleOrDefaultAsync(x => x.Id == id);

        if (project is null)
        {
            return ResultViewModel.Error("Projeto não encontrado");
        }

        project.Update(model.Title, model.Description, model.TotalCost);

        await _dbContext.SaveChangesAsync();

        return ResultViewModel.Success();
    }
}

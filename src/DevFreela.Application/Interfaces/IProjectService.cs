using DevFreela.Application.Models;

namespace DevFreela.Application.Interfaces;

public interface IProjectService
{
    public Task<ResultViewModel<List<ProjectItemViewModel>>> GetAll(string search, int page, int size);
    public Task<ResultViewModel<ProjectViewModel>> GetById(int id);
    public Task<ResultViewModel<int>> Create(CreateProjectInputModel model);
    public Task<ResultViewModel> CreateComment(int id, CreateProjectCommentInputModel model);
    public Task<ResultViewModel> Update(int id, UpdateProjectInputModel model);
    public Task<ResultViewModel> Start(int id);
    public Task<ResultViewModel> Complete(int id);
    public Task<ResultViewModel> Delete(int id);
}

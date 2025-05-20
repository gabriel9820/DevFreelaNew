using DevFreela.Application.Models;
using DevFreela.Core.Entities;
using MediatR;

namespace DevFreela.Application.Commands.CreateProject;

public class CreateProjectCommand : IRequest<ResultViewModel<int>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int ClientId { get; set; }
    public int FreelancerId { get; set; }
    public decimal TotalCost { get; set; }

    public Project ToEntity()
    {
        return new Project(Title, Description, ClientId, FreelancerId, TotalCost);
    }
}

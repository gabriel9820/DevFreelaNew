using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class CreateProjectInputModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ClientId { get; set; }
    public int FreelancerId { get; set; }
    public decimal TotalCost { get; set; }

    public Project ToEntity()
    {
        return new Project(Title, Description, ClientId, FreelancerId, TotalCost);
    }
}

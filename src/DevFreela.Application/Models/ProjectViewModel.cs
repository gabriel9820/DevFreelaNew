using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class ProjectViewModel
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int ClientId { get; private set; }
    public string ClientName { get; private set; }
    public int FreelancerId { get; private set; }
    public string FreelancerName { get; private set; }
    public decimal TotalCost { get; private set; }
    public List<string> Comments { get; private set; }

    public ProjectViewModel(
        int id,
        string title,
        string description,
        int clientId,
        string clientName,
        int freelancerId,
        string freelancerName,
        decimal totalCost,
        List<ProjectComment> comments)
    {
        Id = id;
        Title = title;
        Description = description;
        ClientId = clientId;
        ClientName = clientName;
        FreelancerId = freelancerId;
        FreelancerName = freelancerName;
        TotalCost = totalCost;
        Comments = [.. comments.Select(c => c.Content)];
    }

    public static ProjectViewModel FromEntity(Project project)
    {
        return new ProjectViewModel(
            project.Id,
            project.Title,
            project.Description,
            project.ClientId,
            project.Client.FullName,
            project.FreelancerId,
            project.Freelancer.FullName,
            project.TotalCost,
            project.Comments);
    }
}

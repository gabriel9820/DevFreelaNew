namespace DevFreela.Application.Models;

public class UpdateProjectInputModel
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
}

namespace DevFreela.Application.Models;

public class CreateProjectCommentInputModel
{
    public string Content { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    public int UserId { get; set; }
}

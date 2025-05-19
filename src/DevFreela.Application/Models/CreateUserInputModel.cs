using DevFreela.Core.Entities;

namespace DevFreela.Application.Models;

public class CreateUserInputModel
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime BithDate { get; set; }

    public User ToEntity()
    {
        return new User(FullName, Email, BithDate);
    }
}

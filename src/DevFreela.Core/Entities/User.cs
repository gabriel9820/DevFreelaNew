namespace DevFreela.Core.Entities;

public class User : BaseEntity
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public DateTime BirthDate { get; private set; }
    public bool IsActive { get; private set; }
    public string Password { get; private set; }
    public string Role { get; private set; }
    public List<UserSkill> Skills { get; private set; }
    public List<ProjectComment> Comments { get; private set; }
    public List<Project> OwnedProjects { get; private set; }
    public List<Project> FreelanceProjects { get; private set; }

    protected User() { }

    public User(string fullName, string email, DateTime birthDate, string password, string role)
    {
        FullName = fullName;
        Email = email;
        BirthDate = birthDate;
        Password = password;
        Role = role;
        IsActive = true;

        Skills = [];
        Comments = [];
        OwnedProjects = [];
        FreelanceProjects = [];
    }

    public bool CheckPassword(string hashedPassword)
    {
        return Password == hashedPassword;
    }

    public void UpdatePassword(string hashedPassword)
    {
        Password = hashedPassword;
    }
}

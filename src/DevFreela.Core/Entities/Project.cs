using DevFreela.Core.Enums;

namespace DevFreela.Core.Entities;

public class Project : BaseEntity
{
    public const string INVALID_STATUS_MESSAGE = "O status do projeto é inválido";

    public string Title { get; private set; }
    public string Description { get; private set; }
    public int ClientId { get; private set; }
    public User Client { get; private set; }
    public int FreelancerId { get; private set; }
    public User Freelancer { get; private set; }
    public decimal TotalCost { get; private set; }
    public DateTime? StartedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public ProjectStatusEnum Status { get; private set; }
    public List<ProjectComment> Comments { get; private set; } = [];

    protected Project() { }

    public Project(string title, string description, int clientId, int freelancerId, decimal totalCost)
    {
        Title = title;
        Description = description;
        ClientId = clientId;
        FreelancerId = freelancerId;
        TotalCost = totalCost;

        Status = ProjectStatusEnum.Created;
        Comments = [];
    }

    public void Update(string title, string description, decimal totalCost)
    {
        Title = title;
        Description = description;
        TotalCost = totalCost;
    }

    public void Start()
    {
        if (Status != ProjectStatusEnum.Created)
        {
            throw new InvalidOperationException(INVALID_STATUS_MESSAGE);
        }

        Status = ProjectStatusEnum.InProgress;
        StartedAt = DateTime.UtcNow;
    }

    public void SetPaymentPending()
    {
        if (Status != ProjectStatusEnum.InProgress)
        {
            throw new InvalidOperationException(INVALID_STATUS_MESSAGE);
        }

        Status = ProjectStatusEnum.PaymentPending;
    }

    public void Complete()
    {
        if (Status != ProjectStatusEnum.PaymentPending)
        {
            throw new InvalidOperationException(INVALID_STATUS_MESSAGE);
        }

        Status = ProjectStatusEnum.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Suspend()
    {
        if (Status != ProjectStatusEnum.Created && Status != ProjectStatusEnum.InProgress)
        {
            throw new InvalidOperationException(INVALID_STATUS_MESSAGE);
        }

        Status = ProjectStatusEnum.Suspended;
    }

    public void Cancel()
    {
        if (Status != ProjectStatusEnum.Created && Status != ProjectStatusEnum.Suspended)
        {
            throw new InvalidOperationException(INVALID_STATUS_MESSAGE);
        }

        Status = ProjectStatusEnum.Cancelled;
    }
}

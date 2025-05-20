using MediatR;

namespace DevFreela.Application.Notifications.ProjectCreated;

public class GenerateProjectBoardHandler : INotificationHandler<ProjectCreatedNotification>
{
    public Task Handle(ProjectCreatedNotification notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Criando quadro para o projeto {notification.Title}");

        return Task.CompletedTask;
    }
}

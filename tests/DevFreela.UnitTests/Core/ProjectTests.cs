using DevFreela.Core.Entities;
using DevFreela.Core.Enums;
using FluentAssertions;

namespace DevFreela.UnitTests.Core;

public class ProjectTests
{
    [Fact]
    public void ProjectIsInValidState_Start_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);

        // Act
        project.Start();

        // Assert
        Assert.Equal(ProjectStatusEnum.InProgress, project.Status);
        Assert.NotNull(project.StartedAt);

        project.Status.Should().Be(ProjectStatusEnum.InProgress);
        project.StartedAt.Should().NotBeNull();
    }

    [Fact]
    public void ProjectIsInInvalidState_Start_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);
        project.Suspend();

        // Act + Assert
        Action? action = project.Start;

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);

        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage(Project.INVALID_STATUS_MESSAGE);
    }

    [Fact]
    public void ProjectIsInValidState_SetPaymentPending_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);
        project.Start();

        // Act
        project.SetPaymentPending();

        // Assert
        Assert.Equal(ProjectStatusEnum.PaymentPending, project.Status);
    }

    [Fact]
    public void ProjectIsInInvalidState_SetPaymentPending_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);

        // Act + Assert
        Action? action = project.SetPaymentPending;

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
    }

    [Fact]
    public void ProjectIsInValidState_Complete_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);
        project.Start();
        project.SetPaymentPending();

        // Act
        project.Complete();

        // Assert
        Assert.Equal(ProjectStatusEnum.Completed, project.Status);
        Assert.NotNull(project.CompletedAt);
    }

    [Fact]
    public void ProjectIsInInvalidState_Complete_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);

        // Act + Assert
        Action? action = project.Complete;

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
    }

    [Fact]
    public void ProjectIsInValidState_Suspend_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);

        // Act
        project.Suspend();

        // Assert
        Assert.Equal(ProjectStatusEnum.Suspended, project.Status);
    }

    [Fact]
    public void ProjectIsInInvalidState_Suspend_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);
        project.Start();
        project.SetPaymentPending();

        // Act + Assert
        Action? action = project.Suspend;

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
    }

    [Fact]
    public void ProjectIsInValidState_Cancel_Success()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);

        // Act
        project.Cancel();

        // Assert
        Assert.Equal(ProjectStatusEnum.Cancelled, project.Status);
    }

    [Fact]
    public void ProjectIsInInvalidState_Cancel_ThrowsException()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);
        project.Start();

        // Act + Assert
        Action? action = project.Cancel;

        var exception = Assert.Throws<InvalidOperationException>(action);
        Assert.Equal(Project.INVALID_STATUS_MESSAGE, exception.Message);
    }

    [Fact]
    public void Project_Update_UpdateFields()
    {
        // Arrange
        var project = new Project("Projeto A", "Descrição", 1, 2, 1000);
        var newTitle = "Projeto Novo";
        var newDescription = "Descrição Nova";
        var newTotalCost = 550;

        // Act
        project.Update(newTitle, newDescription, newTotalCost);

        // Assert
        Assert.Equal(newTitle, project.Title);
        Assert.Equal(newDescription, project.Description);
        Assert.Equal(newTotalCost, project.TotalCost);
    }
}

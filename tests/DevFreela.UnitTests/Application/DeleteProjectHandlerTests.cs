using DevFreela.Application.Commands.DeleteProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakers;
using FluentAssertions;
using Moq;

namespace DevFreela.UnitTests.Application;

public class DeleteProjectHandlerTests
{
    [Fact]
    public async Task ProjectExists_Delete_Success()
    {
        // Arrange
        const int SEARCH_ID = 1;
        var expectedProject = new ProjectFaker()
            .RuleFor(p => p.Id, SEARCH_ID)
            .Generate();

        var command = new DeleteProjectCommand(SEARCH_ID);

        var mockRepository = new Mock<IProjectRepository>();
        mockRepository.Setup(r => r.GetByIdAsync(SEARCH_ID)).ReturnsAsync(expectedProject);
        mockRepository.Setup(r => r.UpdateAsync()).Returns(Task.CompletedTask);

        var handler = new DeleteProjectHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);

        result.IsSuccess.Should().BeTrue();

        mockRepository.Verify(r => r.GetByIdAsync(SEARCH_ID), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(), Times.Once);
    }

    [Fact]
    public async Task ProjectDoesNotExist_Delete_Error()
    {
        // Arrange
        const int SEARCH_ID = 1;

        var command = new DeleteProjectCommand(SEARCH_ID);

        var mockRepository = new Mock<IProjectRepository>();
        mockRepository.Setup(r => r.GetByIdAsync(SEARCH_ID)).ReturnsAsync(() => null);

        var handler = new DeleteProjectHandler(mockRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("Projeto não encontrado", result.Message);

        result.IsSuccess.Should().BeFalse();
        result.Message.Should().Be("Projeto não encontrado");

        mockRepository.Verify(r => r.GetByIdAsync(SEARCH_ID), Times.Once);
        mockRepository.Verify(r => r.UpdateAsync(), Times.Never);
    }
}

using DevFreela.Application.Commands.CreateProject;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using DevFreela.UnitTests.Fakers;
using FluentAssertions;
using MediatR;
using Moq;

namespace DevFreela.UnitTests.Application;

public class CreateProjectHandlerTests
{
    [Fact]
    public async Task InputDataIsOk_Create_Success()
    {
        // Arrange
        const int ID = 1;

        var command = new CreateProjectCommandFaker().Generate();

        var mockRepository = new Mock<IProjectRepository>();
        mockRepository.Setup(r => r.CreateAsync(It.IsAny<Project>())).ReturnsAsync(ID);

        var mediator = new Mock<IMediator>();

        var handler = new CreateProjectHandler(mockRepository.Object, mediator.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ID, result.Data);

        result.IsSuccess.Should().BeTrue();
        result.Data.Should().Be(ID);

        mockRepository.Verify(r => r.CreateAsync(It.IsAny<Project>()), Times.Once);
    }
}

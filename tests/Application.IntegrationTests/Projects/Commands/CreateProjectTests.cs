using Application.Projects.Commands.CreateProject;
using Domain.Enums;
using FluentAssertions;

namespace Application.IntegrationTests.Projects.Commands;
public class CreateProjectTests : BaseClassFixture
{
    public CreateProjectTests(CustomApiFactory apiFactory) : base(apiFactory)
    {
    }
    [Fact]
    public async Task ShouldCreateProject_WhenDataIsValid()
    {
        // Arrange
        var command = new CreateProjectCommand
        {
            Name = "Heo beo",
            Description = "rat beo",
            OwnerId = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now
        };
        
        // Act
        var result = await SendAsync(command);
        
        // Assert
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
        result.OwnerId.Should().Be(command.OwnerId);
        result.Status.Should().Be(ProjectStatus.Planning.ToString());
        result.StartDate.Should().Be(command.StartDate);
        result.EndDate.Should().Be(command.EndDate);
    }
}
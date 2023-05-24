using Application.Projects.Commands.CreateProject;
using Application.Projects.Queries.GetProjectById;
using FluentAssertions;

namespace Application.IntegrationTests.Projects.Queries;

public class GetProjectByIdTests : BaseClassFixture
{

    public GetProjectByIdTests(CustomApiFactory apiFactory) : base(apiFactory)
    {
    }

    [Fact]
    public async Task ShouldReturnProject_WhenThatProjectExists()
    {
        // Arrange
        var ownerId = Guid.NewGuid();
        var createCommand = new CreateProjectCommand
        {
            OwnerId = ownerId,
            Name = "Abc",
            Description = "123",
            StartDate = DateTime.Today,
            EndDate = DateTime.Today
        };
        var createdProject = await SendAsync(createCommand);
        var query = new GetProjectByIdQuery
        {
            ProjectId = createdProject.Id,
            UserId = ownerId
        };
        
        // Act
        var result = await SendAsync(query);

        // Assert
        result.Should().BeEquivalentTo(createdProject);
    }
}
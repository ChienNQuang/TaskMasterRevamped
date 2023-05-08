using Application.Projects.Commands.CreateProject;
using Application.Projects.Queries.GetProjectById;
using Domain.Enums;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Projects.Commands;

public class CreateProjectTests : IClassFixture<CustomApiFactory>
{
    private readonly IMediator _mediator;

    public CreateProjectTests(CustomApiFactory apiFactory)
    {
        _mediator = apiFactory.Services.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task ShouldReturnProject_WhenThatProjectExists()
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
        var result = await _mediator.Send(command);
        
        // Assert
        result.Name.Should().Be(command.Name);
        result.Description.Should().Be(command.Description);
        result.OwnerId.Should().Be(command.OwnerId);
        result.Status.Should().Be(ProjectStatus.Planning.ToString());
        result.StartDate.Should().Be(command.StartDate);
        result.EndDate.Should().Be(command.EndDate);
    }
}
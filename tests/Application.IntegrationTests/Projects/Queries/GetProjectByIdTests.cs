using Application.Projects.Queries.GetProjectById;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.IntegrationTests.Projects.Queries;

public class GetProjectByIdTests : IClassFixture<CustomApiFactory>
{
    private readonly IMediator _mediator;

    public GetProjectByIdTests(CustomApiFactory apiFactory)
    {
        _mediator = apiFactory.Services.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task ShouldReturnProject_WhenThatProjectExists()
    {
        
        var query = new GetProjectByIdQuery
        {
            ProjectId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };
        var result = await _mediator.Send(query);
    }
}
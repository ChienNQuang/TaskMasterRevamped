using Application.Projects.Queries.GetProjectById;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("users/{userId:guid}/[controller]")]
public class ProjectsController : ApiControllerBase
{
    [HttpGet("{projectId:guid}")]
    public async Task<ActionResult<ProjectResult>> GetProjectById([FromQuery] Guid userId, [FromQuery] Guid projectId)
    {
        var query = new GetProjectByIdQuery
        {
            ProjectId = projectId,
            UserId = userId
        };
        return await Mediator.Send(query);
    }
}
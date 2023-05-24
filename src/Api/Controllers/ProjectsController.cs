using Application.Projects;
using Application.Projects.Queries.GetProjectById;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
public class ProjectsController : ApiControllerBase
{
    [HttpGet("{projectId:guid}")]
    public async Task<ActionResult<ProjectResult>> GetProjectById([FromQuery] Guid projectId)
    {
        var query = new GetProjectByIdQuery
        {
            ProjectId = projectId,
            UserId = Guid.Empty
        };
        return await Mediator.Send(query);
    }
}
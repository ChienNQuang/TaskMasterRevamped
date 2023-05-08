using Application.Common.Mapping;
using Domain.Entities;

namespace Application.Projects.Queries.GetProjectById;

public class ProjectResult : IMapFrom<Project>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public DateOnly CreationDate { get; set; }
}
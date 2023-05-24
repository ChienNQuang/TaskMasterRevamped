using Application.Common.Mapping;
using AutoMapper;
using Domain.Entities;

namespace Application.Projects;

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

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Project, ProjectResult>()
            .ForMember(p => p.StartDate,
                opt => opt.MapFrom(src => src.StartDate.ToDateTimeUnspecified()))
            .ForMember(p => p.EndDate,
                opt => opt.MapFrom(src => src.EndDate.ToDateTimeUnspecified()))
            .ForMember(p => p.CreationDate,
                opt => opt.MapFrom(src => src.CreationDate.ToDateOnly()));
    }
}
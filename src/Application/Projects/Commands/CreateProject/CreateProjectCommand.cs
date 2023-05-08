using Application.Common.Interfaces;
using Application.Projects.Queries.GetProjectById;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using NodaTime;
using NodaTime.Extensions;

namespace Application.Projects.Commands.CreateProject;

public record class CreateProjectCommand : IRequest<ProjectResult>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid OwnerId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateProjectCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ProjectResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var entity = new Project
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = request.OwnerId,
            StartDate = LocalDateTime.FromDateTime(request.StartDate),
            EndDate = LocalDateTime.FromDateTime(request.EndDate),
            Status = ProjectStatus.Planning,
            CreationDate = SystemClock.Instance.GetCurrentInstant().ToDateTimeUtc().ToLocalDateTime().Date
        };

        var result = await _context.Projects.AddAsync(entity);

        return _mapper.Map<ProjectResult>(result);
    }
}
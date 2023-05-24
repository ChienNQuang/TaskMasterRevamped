using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Projects.Queries.GetProjectById;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NodaTime.Extensions;

namespace Application.Projects.Commands.CreateProject;

public record CreateProjectCommand : IRequest<ProjectResult>
{
    public string Name { get; init; }
    public string Description { get; init; }
    public Guid OwnerId { get; init; }
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }

    public Project ToProject()
    {
        return new Project
        {
            Name = Name,
            Description = Description,
            OwnerId = OwnerId,
            StartDate = LocalDateTime.FromDateTime(StartDate),
            EndDate = LocalDateTime.FromDateTime(EndDate),
            Status = ProjectStatus.Planning,
            CreationDate = SystemClock.Instance.GetCurrentInstant().ToDateTimeUtc().ToLocalDateTime().Date
        };
    }
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
        var entity = request.ToProject();

        var result = await _context.Projects.AddAsync(entity, cancellationToken);
        var addedResult = await _context.SaveChangesAsync(cancellationToken);
        if (addedResult <= 0)
        {
            throw new DatabaseException();
        }
        return _mapper.Map<ProjectResult>(result.Entity);
    }
}
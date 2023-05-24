using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Projects.Queries.GetProjectById;

public record GetProjectByIdQuery : IRequest<ProjectResult>
{
    public Guid UserId { get; init; }
    public Guid ProjectId { get; init; }    
}

public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectResult>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetProjectByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    
    public async Task<ProjectResult> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
    {
        var project = await _context.Projects
            .FirstOrDefaultAsync(p => p.Id.Equals(request.ProjectId) && p.OwnerId.Equals(request.UserId), cancellationToken: cancellationToken);
        if (project is null)
        {
            throw new NotFoundException(nameof(project), request.ProjectId);
        }

        return _mapper.Map<ProjectResult>(project);
    }
}
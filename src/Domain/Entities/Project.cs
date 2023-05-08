using Domain.Enums;
using NodaTime;

namespace Domain.Entities;

public class Project
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid OwnerId { get; set; }
    public LocalDateTime StartDate { get; set; }
    public LocalDateTime EndDate { get; set; }
    public ProjectStatus Status { get; set; }
    public LocalDate CreationDate { get; set; }
}
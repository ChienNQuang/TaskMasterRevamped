using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.Database;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Project> Projects { get; set; }
}
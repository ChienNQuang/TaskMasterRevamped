using Api;
using Api.Database;
using Application.Common.Interfaces;
using Application.Projects.Queries.GetProjectById;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace Application.IntegrationTests;

public class CustomApiFactory : WebApplicationFactory<IApiMarker>
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("test")
        .WithUsername("heobeo")
        .WithPassword("beobeobeo")
        .Build();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblyContaining<GetProjectByIdQuery>();
            });

            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(_dbContainer.GetConnectionString());
            });
        });
    }
    
}
using Api;
using Application.Common.Interfaces;
using Application.Projects.Queries.GetProjectById;
using Domain.Entities;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;

namespace Application.IntegrationTests;

public class CustomApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithDatabase("tmtest")
        .WithUsername("something")
        .WithPassword("somethingelse")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<ApplicationDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddDbContext<ApplicationDbContext>(optionsBuilder =>
            {
                optionsBuilder.UseNpgsql(_dbContainer.GetConnectionString(), opt => opt.UseNodaTime());
            });
        });
    }

    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
    }
    

    public new async Task DisposeAsync()
    {
        await _dbContainer.StopAsync();
    }
}
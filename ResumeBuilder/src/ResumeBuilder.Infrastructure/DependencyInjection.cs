using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResumeBuilder.Application.Contracts;
using ResumeBuilder.Infrastructure.Data;
using ResumeBuilder.Infrastructure.Repositories;

namespace ResumeBuilder.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ResumeBuilderConnection")
            ?? throw new InvalidOperationException("Connection string 'ResumeBuilderConnection' was not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IResumeRepository, ResumeRepository>();

        return services;
    }
}

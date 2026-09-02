using Microsoft.Extensions.DependencyInjection;
using ResumeBuilder.Application.Contracts;
using ResumeBuilder.Application.Services;

namespace ResumeBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IResumeService, ResumeService>();
        return services;
    }
}

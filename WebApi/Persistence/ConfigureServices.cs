using MyTaskBoard.Application.Common.Interfaces;
using MyTaskBoard.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using MyTaskBoard.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MyTaskBoard.Persistence.Initialization;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("NSwag")))? configuration.GetConnectionString("DefaultConnection") : configuration.GetConnectionString("DesignTimeConnection");
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitySaveChangesInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

                options.UseSqlServer(connectionString);
        });
        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}

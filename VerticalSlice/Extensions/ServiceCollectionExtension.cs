using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VerticalSlice.Behaviors;
using VerticalSlice.Middleware;

namespace VerticalSlice.Extensions;

public static class ServiceCollectionExtension
{
    /// <summary>
    /// All the possible service methods are called and injected
    /// in the di
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddInfraStructure(this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        services.IocServicesInAssembly(configuration, assembly);
        
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.Configure<ApiBehaviorOptions>(x =>
        {
            x.SuppressModelStateInvalidFilter = true;
        });

        services.AddInfrastructureValidation(assembly);
        services.AddTransient<ExceptionHandlingMiddleware>();
        
        return services;
    }
}
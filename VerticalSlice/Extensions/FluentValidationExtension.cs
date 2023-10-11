using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace VerticalSlice.Extensions;

public static class FluentValidationExtension
{
    public static IServiceCollection AddInfrastructureValidation(this IServiceCollection services, Assembly assembly)
    {
        services.AddValidatorsFromAssembly(assembly);
        
        var serviceDescriptors = services
            .Where(descriptor => typeof(IValidator) != descriptor.ServiceType
                                 && typeof(IValidator).IsAssignableFrom(descriptor.ServiceType)
                                 && descriptor.ServiceType.IsInterface)
            .ToList();
        
        foreach (var descriptor in serviceDescriptors)
        {
            services.Add(new ServiceDescriptor(
                typeof(IValidator),
                p => p.GetRequiredService(descriptor.ServiceType),
                descriptor.Lifetime));
        }
        return services;
    }
}
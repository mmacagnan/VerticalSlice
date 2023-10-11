using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VerticalSlice.Abstraction.Ioc;

namespace VerticalSlice.Extensions;

public static class IocServicesExtension
{
    public static IServiceCollection IocServicesInAssembly(this IServiceCollection services,
        IConfiguration configuration,
        Assembly assembly)
    {
        var list = assembly.ExportedTypes
            .Where(x => typeof(IIocConfig).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IIocConfig>()
            .ToList();

        list.ForEach(i => i.IocServiceInstall(services, configuration));
        
        return services;
    }
}
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VerticalSlice.Abstraction.Ioc;

namespace VerticalSlice.Extensions;

public static class IocServicesExtension
{
    /// <summary>
    /// Call IocServiceInstall method of all
    /// classes that inherits <see cref="IIocConfig"/> interface
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection IocServicesInAssembly(
        this IServiceCollection services,
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
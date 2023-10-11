using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace VerticalSlice.Abstraction.Ioc;

public interface IIocConfig
{
    void IocServiceInstall(IServiceCollection services, IConfiguration configuration);
}
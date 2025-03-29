using MattEland.Emergence.World.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MattEland.Emergence.DesktopClient;

public static class DependencyInjectionConfiguration
{
    public static ServiceProvider BuildServiceProvider()
    {
        ServiceCollection services = new ServiceCollection();
     
        services.AddScoped<EmergenceGame>();
        services.AddScoped<IWorldService, WorldService>();

        return services.BuildServiceProvider();
    }
}
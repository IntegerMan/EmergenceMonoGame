using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MattEland.Emergence.DesktopClient.Configuration;

public static class DependencyInjectionConfiguration
{
    public static ServiceProvider BuildServiceProvider(IConfiguration configuration)
    {
        ServiceCollection services = new();

        services.AddScoped<EmergenceGame>();
        services.AddScoped<IWorldService, WorldService>();
        services.AddScoped<ILevelGenerator, TestLevelGenerator>();

        // Bind GraphicsSettings to the "Graphics" node in configuration
        services.Configure<GraphicsSettings>(options => configuration.GetSection("Graphics").Bind(options));

        return services.BuildServiceProvider();
    }
}
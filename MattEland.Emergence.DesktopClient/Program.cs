using System.Diagnostics.CodeAnalysis;
using MattEland.Emergence.DesktopClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MattEland.Emergence.DesktopClient;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
    Justification = "This is the entry point for the application")]
public class Program
{
    public static void Main(string[] args)
    {
        IConfiguration configuration = OptionsConfiguration.BuildConfiguration();
        using ServiceProvider provider = DependencyInjectionConfiguration.BuildServiceProvider(configuration);

        using EmergenceGame game = provider.GetRequiredService<EmergenceGame>();
        game.Run();
    }
}
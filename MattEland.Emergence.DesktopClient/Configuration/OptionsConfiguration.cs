using Microsoft.Extensions.Configuration;

namespace MattEland.Emergence.DesktopClient.Configuration;

public static class OptionsConfiguration
{
    public static IConfiguration BuildConfiguration(string[] args)
    {
        ConfigurationBuilder configurationBuilder = new();

        configurationBuilder.AddCommandLine(args);
        configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        // Could add environment variables, and user secrets if there was a reason to

        return configurationBuilder.Build();
    }
}
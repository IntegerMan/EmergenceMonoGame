using Microsoft.Extensions.Configuration;

namespace MattEland.Emergence.DesktopClient.Configuration;

public static class OptionsConfiguration
{
    public static IConfiguration BuildConfiguration()
    {
        ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
        // Could add command line, environment variables, and user secrets if there was a reason to

        return configurationBuilder.Build();
    }
}
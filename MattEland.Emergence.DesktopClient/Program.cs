using MattEland.Emergence.DesktopClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);
IConfiguration configuration = configurationBuilder.Build();

ServiceProvider provider = DependencyInjectionConfiguration.BuildServiceProvider(configuration);

using EmergenceGame game = provider.GetRequiredService<EmergenceGame>();
game.Run();
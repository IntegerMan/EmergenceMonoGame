using MattEland.Emergence.DesktopClient;
using MattEland.Emergence.DesktopClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

IConfiguration configuration = OptionsConfiguration.BuildConfiguration();
using ServiceProvider provider = DependencyInjectionConfiguration.BuildServiceProvider(configuration);

using EmergenceGame game = provider.GetRequiredService<EmergenceGame>();
game.Run();
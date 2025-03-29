using MattEland.Emergence.DesktopClient;
using Microsoft.Extensions.DependencyInjection;

ServiceProvider provider = DependencyInjectionConfiguration.BuildServiceProvider();

using EmergenceGame game = provider.GetRequiredService<EmergenceGame>();
game.Run();
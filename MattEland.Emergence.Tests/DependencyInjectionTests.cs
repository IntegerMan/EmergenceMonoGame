using MattEland.Emergence.DesktopClient;
using MattEland.Emergence.DesktopClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MattEland.Emergence.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void GameShouldBeCreatableGivenGameDependencies()
    {
        // Arrange
        ConfigurationBuilder configBuilder = new();
        IConfiguration config = configBuilder.Build();
        ServiceProvider services = DependencyInjectionConfiguration.BuildServiceProvider(config);

        // Act
        using EmergenceGame game = services.GetRequiredService<EmergenceGame>();

        // Assert
        game.ShouldNotBeNull();
    }
}
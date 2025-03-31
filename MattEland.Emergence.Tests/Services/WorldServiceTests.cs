using Moq;

namespace MattEland.Emergence.Tests.Services;

[TestSubject(typeof(WorldService))]
public class WorldServiceTests
{
    [Fact]
    public void CreatePlayer_ShouldInitializePlayerAtOrigin()
    {
        // Arrange
        WorldService service = CreateWorldService();

        // Act
        Player player = service.Player;

        // Assert
        player.ShouldNotBeNull();
        player.Pos.ShouldBe(new WorldPos(0, 0));
    }
    
    [Fact]
    public void StartWorldShouldSetLevelAndPlayer()
    {
        // Arrange
        WorldService service = CreateWorldService();

        // Act
        (Level level, Player player) = service.StartWorld();

        // Assert
        player.ShouldNotBeNull();
        level.ShouldNotBeNull();
        service.Level.ShouldNotBeNull();
        service.Player.ShouldNotBeNull();
    }

    private static WorldService CreateWorldService()
    {
        Level testLevel = new(10, 10);
        
        Mock<ILevelGenerator> levelGeneratorMock = new();
        levelGeneratorMock.Setup(x => x.Generate(It.IsAny<Player>()))
            .Returns(testLevel);

        return new WorldService(levelGeneratorMock.Object);
    }

    [Fact]
    public void GetVisibleObjects_ShouldReturnCorrectViewportData()
    {
        // Arrange
        WorldService service = CreateWorldService();
        service.StartWorld();
        ViewportDimensions viewport = new(5, 5);

        // Act
        ViewportData viewportData = service.GetVisibleObjects(viewport);

        // Assert
        viewportData.ShouldNotBeNull();
        viewportData.Viewport.ShouldBe(viewport);
        viewportData.Center.ShouldBe(service.Player.Pos);
        viewportData.VisibleTiles.Count().ShouldBe(viewport.Width * viewport.Height);
    }
}
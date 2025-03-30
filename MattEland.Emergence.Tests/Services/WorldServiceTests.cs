namespace MattEland.Emergence.Tests.Services;

[TestSubject(typeof(WorldService))]
public class WorldServiceTests
{
    [Fact]
    public void CreatePlayer_ShouldInitializePlayerAtOrigin()
    {
        // Arrange
        WorldService service = new();

        // Act
        Player player = service.CreatePlayer();

        // Assert
        player.ShouldNotBeNull();
        player.Pos.ShouldBe(new WorldPos(0, 0));
    }

    [Fact]
    public void CreateLevel_ShouldInitializeLevelWithPlayer()
    {
        // Arrange
        WorldService service = new();
        Player player = service.CreatePlayer();

        // Act
        Level level = service.CreateLevel(player);

        // Assert
        level.ShouldNotBeNull();
        level.Objects.ShouldContain(player);
        player.Pos.ShouldBe(new WorldPos(2, 3));
    }

    [Fact]
    public void GetVisibleObjects_ShouldReturnCorrectViewportData()
    {
        // Arrange
        WorldService service = new();
        Player player = new(new WorldPos(5, 5));
        Level level = new(10, 10);
        ViewportDimensions viewport = new(5, 5);

        // Act
        ViewportData viewportData = service.GetVisibleObjects(player, level, viewport);

        // Assert
        viewportData.ShouldNotBeNull();
        viewportData.Viewport.ShouldBe(viewport);
        viewportData.Center.ShouldBe(player.Pos);
        viewportData.VisibleTiles.Count().ShouldBe(viewport.Width * viewport.Height);
    }
}
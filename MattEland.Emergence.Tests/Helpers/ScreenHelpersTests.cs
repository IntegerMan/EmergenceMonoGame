using MattEland.Emergence.DesktopClient;

namespace MattEland.Emergence.Tests.Helpers;

[TestSubject(typeof(ScreenHelpers))]
public class ScreenHelpersTests
{
    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForPositiveCoordinates()
    {
        // Arrange
        WorldPos pos = new WorldPos(2, 3);

        // Act
        Rectangle rect = pos.ToRectangle();

        // Assert
        rect.ShouldBe(new Rectangle(2 * EmergenceGame.TileSize, 3 * EmergenceGame.TileSize, EmergenceGame.TileSize,
            EmergenceGame.TileSize));
    }

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForNegativeCoordinates()
    {
        // Arrange
        WorldPos pos = new WorldPos(-1, -2);

        // Act
        Rectangle rect = pos.ToRectangle();

        // Assert
        rect.ShouldBe(new Rectangle(-1 * EmergenceGame.TileSize, -2 * EmergenceGame.TileSize, EmergenceGame.TileSize,
            EmergenceGame.TileSize));
    }

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForZeroCoordinates()
    {
        // Arrange
        WorldPos pos = new WorldPos(0, 0);

        // Act
        Rectangle rect = pos.ToRectangle();

        // Assert
        rect.ShouldBe(new Rectangle(0, 0, EmergenceGame.TileSize, EmergenceGame.TileSize));
    }
}
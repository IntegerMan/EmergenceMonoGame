namespace MattEland.Emergence.Tests.Helpers;

[TestSubject(typeof(ScreenHelpers))]
public class ScreenHelpersTests
{
    private const int TileSize = 32;

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForPositiveCoordinates()
    {
        // Arrange
        WorldPos pos = new(2, 3);
        Point offset = new(0, 0);
        
        // Act
        Rectangle rect = pos.ToRectangle(TileSize, offset);

        // Assert
        rect.ShouldBe(new Rectangle(2 * TileSize, 3 * TileSize, TileSize, TileSize));
    }

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForNegativeCoordinates()
    {
        // Arrange
        WorldPos pos = new(-1, -2);
        Point offset = new(0, 0);
        
        // Act
        Rectangle rect = pos.ToRectangle(TileSize, offset);

        // Assert
        rect.ShouldBe(new Rectangle(-1 * TileSize, -2 * TileSize, TileSize, TileSize));
    }

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForZeroCoordinates()
    {
        // Arrange
        WorldPos pos = new(0, 0);
        Point offset = new(0, 0);

        // Act
        Rectangle rect = pos.ToRectangle(TileSize, offset);

        // Assert
        rect.ShouldBe(new Rectangle(0, 0, TileSize, TileSize));
    }
}
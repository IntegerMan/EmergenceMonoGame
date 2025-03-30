using MattEland.Emergence.DesktopClient;

namespace MattEland.Emergence.Tests.Helpers;

[TestSubject(typeof(ScreenHelpers))]
public class ScreenHelpersTests
{
    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForPositiveCoordinates()
    {
        WorldPos pos = new WorldPos(2, 3);
        Rectangle rect = pos.ToRectangle();
        Assert.Equal(
            new Rectangle(2 * EmergenceGame.TileSize, 3 * EmergenceGame.TileSize, EmergenceGame.TileSize,
                EmergenceGame.TileSize), rect);
    }

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForNegativeCoordinates()
    {
        WorldPos pos = new WorldPos(-1, -2);
        Rectangle rect = pos.ToRectangle();
        Assert.Equal(
            new Rectangle(-1 * EmergenceGame.TileSize, -2 * EmergenceGame.TileSize, EmergenceGame.TileSize,
                EmergenceGame.TileSize), rect);
    }

    [Fact]
    public void ToRectangle_ReturnsCorrectRectangle_ForZeroCoordinates()
    {
        WorldPos pos = new WorldPos(0, 0);
        Rectangle rect = pos.ToRectangle();
        Assert.Equal(new Rectangle(0, 0, EmergenceGame.TileSize, EmergenceGame.TileSize), rect);
    }
}
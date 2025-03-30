namespace MattEland.Emergence.Tests.Helpers;

[TestSubject(typeof(ColorHelpers))]
public class ColorHelpersTests
{
    [Fact]
    public void GetTileColor_ReturnsBlack_ForVoidFloor()
    {
        // Arrange
        TileInfo tile = new() { Floor = FloorType.Void };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.Black);
    }

    [Fact]
    public void GetTileColor_ReturnsGray_ForNormalFloor()
    {
        // Arrange
        TileInfo tile = new() { Floor = FloorType.Normal };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.Gray);
    }

    [Fact]
    public void GetTileColor_ReturnsMagenta_ForVirusFloor()
    {
        // Arrange
        TileInfo tile = new() { Floor = FloorType.Virus };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.Magenta);
    }

    [Fact]
    public void GetTileColor_ReturnsDimGray_ForWildernessFloor()
    {
        // Arrange
        TileInfo tile = new() { Floor = FloorType.Wilderness };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.DimGray);
    }

    [Fact]
    public void GetTileColor_Throws_ForUnknownFloor()
    {
        Should.Throw<NotSupportedException>(() =>
        {
            // Arrange
            TileInfo tile = new() { Floor = (FloorType)999 };

            // Act
            tile.GetTileColor();
        });
    }
}
namespace MattEland.Emergence.Tests.Helpers;

[TestSubject(typeof(ColorHelpers))]
public class ColorHelpersTests
{
    [Fact]
    public void GetTileColor_ReturnsBlack_ForVoidFloor()
    {
        // Arrange
        TileInfo tile = new()
        {
            Floor = FloorType.Void,
            Pos = new WorldPos(0, 0)
        };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.Black);
    }

    [Fact]
    public void GetTileColor_ReturnsGray_ForNormalFloor()
    {
        // Arrange
        TileInfo tile = new()
        {
            Floor = FloorType.Normal,
            Pos = new WorldPos(0, 0)
        };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.Gray);
    }

    [Fact]
    public void GetTileColor_ReturnsMagenta_ForVirusFloor()
    {
        // Arrange
        TileInfo tile = new()
        {
            Floor = FloorType.Virus,
            Pos = new WorldPos(0, 0)
        };

        // Act
        Color color = tile.GetTileColor();

        // Assert
        color.ShouldBe(Color.Magenta);
    }

    [Fact]
    public void GetTileColor_ReturnsDimGray_ForWildernessFloor()
    {
        // Arrange
        TileInfo tile = new()
        {
            Floor = FloorType.Wilderness,
            Pos = new WorldPos(0, 0)
        };

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
            TileInfo tile = new()
            {
                Floor = (FloorType)999,
                Pos = new WorldPos(0, 0)
            };

            // Act
            tile.GetTileColor();
        });
    }
}
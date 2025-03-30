namespace MattEland.Emergence.Tests.Helpers;

[TestSubject(typeof(ColorHelpers))]
public class ColorHelpersTests
{
    [Fact]
    public void GetTileColor_ReturnsBlack_ForVoidFloor()
    {
        TileInfo tile = new() { Floor = FloorType.Void };
        Color color = tile.GetTileColor();
        color.ShouldBe(Color.Black);
    }

    [Fact]
    public void GetTileColor_ReturnsGray_ForNormalFloor()
    {
        TileInfo tile = new() { Floor = FloorType.Normal };
        Color color = tile.GetTileColor();
        color.ShouldBe(Color.Gray);
    }

    [Fact]
    public void GetTileColor_ReturnsMagenta_ForVirusFloor()
    {
        TileInfo tile = new() { Floor = FloorType.Virus };
        Color color = tile.GetTileColor();
        color.ShouldBe(Color.Magenta);
    }

    [Fact]
    public void GetTileColor_ReturnsDimGray_ForWildernessFloor()
    {
        TileInfo tile = new() { Floor = FloorType.Wilderness };
        Color color = tile.GetTileColor();
        color.ShouldBe(Color.DimGray);
    }

    [Fact]
    public void GetTileColor_Throws_ForUnknownFloor()
    {
        Should.Throw<NotSupportedException>(() =>
        {
            TileInfo tile = new() { Floor = (FloorType)999 };
            tile.GetTileColor();
        });
    }
}
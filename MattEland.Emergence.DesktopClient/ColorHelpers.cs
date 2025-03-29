using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient;

public static class ColorHelpers
{
    public static Color GetTileColor(this TileInfo tile)
    {
        return tile.Floor switch
        {
            FloorType.Void => Color.Black,
            FloorType.Normal => Color.Gray,
            FloorType.Virus => Color.Magenta,
            FloorType.Wilderness => Color.DimGray,
            _ => Color.White
        };
    }
}
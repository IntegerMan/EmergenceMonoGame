using System;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient.Helpers;

public static class ColorHelpers
{
    public static Color VoidColor => Color.Purple;
    public static Color NormalColor => Color.Gray;
    public static Color VirusColor => Color.Magenta;
    public static Color WildernessColor => Color.DimGray;
    
    public static Color GetTileColor(this TileInfo tile)
    {
        return tile.Floor switch
        {
            FloorType.Void => VoidColor,
            FloorType.Normal => NormalColor,
            FloorType.Virus => VirusColor,
            FloorType.Wilderness => WildernessColor,
            _ => throw new NotSupportedException($"Unknown floor type {tile.Floor}")
        };
    }
}
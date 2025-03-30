using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MattEland.Emergence.DesktopClient.Helpers;

public static class ScreenHelpers
{
    public static Rectangle ToRectangle(this WorldPos pos, int tileSize)
        => new(pos.X * tileSize, pos.Y * tileSize, tileSize, tileSize);
    
    public static Point ToPoint(this WorldPos pos, int tileSize)
        => new(pos.X * tileSize, pos.Y * tileSize);
}
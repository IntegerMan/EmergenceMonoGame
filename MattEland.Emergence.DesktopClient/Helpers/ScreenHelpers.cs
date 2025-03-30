using System.Diagnostics.CodeAnalysis;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MattEland.Emergence.DesktopClient.Helpers;

public static class ScreenHelpers
{
    [SuppressMessage("ReSharper", "ArrangeRedundantParentheses", Justification = "Readability")]
    public static Rectangle ToRectangle(this WorldPos pos, int tileSize, Point offset) 
        => new((pos.X * tileSize) + offset.X, (pos.Y * tileSize) + offset.Y, tileSize, tileSize);

    public static Point ToPoint(this WorldPos pos, int tileSize)
        => new(pos.X * tileSize, pos.Y * tileSize);
    
    public static Point ToOffset(this WorldPos pos, int tileSize)
        => new(pos.X * tileSize * -1, pos.Y * tileSize * -1);
}
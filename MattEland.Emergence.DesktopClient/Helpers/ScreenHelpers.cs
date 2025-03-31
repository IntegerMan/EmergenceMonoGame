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

    private static Point ToPoint(this WorldPos pos, int tileSize)
        => new(pos.X * tileSize, pos.Y * tileSize);
    
    public static Point ToOffset(this WorldPos pos, int tileSize)
    {
        // If we never use ToPoint beyond this we'll want to streamline this and remove ToPoint
        Point point = pos.ToPoint(tileSize);
        return new Point(-point.X, -point.Y);
    }
}
using MattEland.Emergence.World.Models;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace MattEland.Emergence.DesktopClient.Helpers;

public static class ScreenHelpers
{
    public static Rectangle ToRectangle(this WorldPos pos)
        => new(pos.X * EmergenceGame.TileSize, pos.Y * EmergenceGame.TileSize, EmergenceGame.TileSize,
            EmergenceGame.TileSize);
}
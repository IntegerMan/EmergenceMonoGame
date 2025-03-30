using System.Drawing;

namespace MattEland.Emergence.World.Models;

public class ViewportData
{
    public required WorldPos Center { get; init; }
    public required ViewportDimensions Viewport { get; init; }
    public required IEnumerable<TileInfo> VisibleTiles { get; init; }
    public required IEnumerable<GameObject> VisibleObjects { get; init; }
    public WorldPos UpperLeft => Center.Offset(-Viewport.Width / 2, -Viewport.Height / 2);
}
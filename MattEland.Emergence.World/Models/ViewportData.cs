using MattEland.Emergence.World.Entities;

namespace MattEland.Emergence.World.Models;

public class ViewportData
{
    public required Pos2D Center { get; init; }
    public required ViewportDimensions Viewport { get; init; }
    public required IEnumerable<TileInfo> VisibleTiles { get; init; }
    public required IEnumerable<GameObject> VisibleObjects { get; init; }
    public Pos2D UpperLeft => Center.Offset(-Viewport.Width / 2, -Viewport.Height / 2);
}
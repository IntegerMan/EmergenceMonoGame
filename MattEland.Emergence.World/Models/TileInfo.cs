namespace MattEland.Emergence.World.Models;

public record TileInfo
{
    public required Pos2D Pos { get; init; }
    public FloorType Floor { get; init; }
}
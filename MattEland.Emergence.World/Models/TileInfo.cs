namespace MattEland.Emergence.World.Models;

public record TileInfo()
{
    public WorldPos Pos { get; init; }
    public FloorType Floor { get; init; }
}
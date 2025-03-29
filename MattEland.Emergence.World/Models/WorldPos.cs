namespace MattEland.Emergence.World.Models;

public record WorldPos(int X, int Y)
{
    public WorldPos Offset(int x, int y) => new(X + x, Y + y);
}
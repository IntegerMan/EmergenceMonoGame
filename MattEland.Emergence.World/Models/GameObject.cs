namespace MattEland.Emergence.World.Models;

public abstract class GameObject(WorldPos pos)
{
    public WorldPos Pos { get; set; } = pos;
}
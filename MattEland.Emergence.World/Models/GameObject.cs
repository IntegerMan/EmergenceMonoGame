namespace MattEland.Emergence.World.Models;

public abstract class GameObject(Pos2D pos)
{
    public Pos2D Pos { get; set; } = pos;
}
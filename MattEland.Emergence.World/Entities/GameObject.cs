using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Entities;

public abstract class GameObject(Pos2D pos)
{
    public Pos2D Pos { get; set; } = pos;
    public abstract string ForegroundColor { get; }
}
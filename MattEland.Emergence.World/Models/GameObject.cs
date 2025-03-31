using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.World.Models;

public abstract class GameObject(Pos2D pos)
{
    public Pos2D Pos { get; set; } = pos;
    public abstract string ForegroundColor { get; }
    public abstract char AsciiChar { get; }
    
    public abstract GameObjectType ObjectType { get; }
    public virtual bool BlocksSight => false;
    public virtual bool IsInvulnerable => false;
}
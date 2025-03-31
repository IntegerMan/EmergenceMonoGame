using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.World.Models;

public abstract class Actor(Pos2D pos, ActorType actorType) : GameObject(pos)
{
    public bool IsPlayer => actorType == ActorType.Player;
}
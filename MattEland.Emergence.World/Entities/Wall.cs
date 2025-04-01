using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;

namespace MattEland.Emergence.World.Entities;

public class Wall(Pos2D pos) : GameObject(pos)
{
    public override string ForegroundColor => GameColors.Gray;
}
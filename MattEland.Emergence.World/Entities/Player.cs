using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;

namespace MattEland.Emergence.World.Entities;

public class Player(Pos2D pos) : GameObject(pos)
{
    public override string ForegroundColor => GameColors.Green;
}
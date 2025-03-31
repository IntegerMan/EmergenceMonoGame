namespace MattEland.Emergence.World.Models;

public class Player(Pos2D pos) : Actor(pos, ActorType.Player)
{
    public override string ForegroundColor => GameColors.Green;
    public override char AsciiChar => '@';
}
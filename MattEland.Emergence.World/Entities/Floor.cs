using MattEland.Emergence.LevelData;
using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Entities;

public class Floor : GameObject
{
    public Floor(Pos2D pos, FloorType floorType) : base(pos)
    {
        FloorType = floorType;
    }

    public FloorType FloorType { get; }

    public override char AsciiChar
    {
        get
        {
            switch (FloorType)
            {
                case FloorType.DecorativeTile:
                    return '\'';
                case FloorType.Walkway:
                    return '_';
                case FloorType.CautionMarker:
                    return '=';
                default:
                    return '.';
            }
        }
    }

    public override string ForegroundColor {
        get
        {
            switch (FloorType)
            {
                case FloorType.DecorativeTile: return GameColors.LightGray;
                case FloorType.CautionMarker: return GameColors.LightYellow;
                case FloorType.Walkway: return GameColors.White;
                default: return GameColors.Gray;
            }
        }
    }

    /*
    public override int ZIndex => 0;

    public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
    {
        context.MoveObject(actor, Pos);

        return true;
    }
    */
}
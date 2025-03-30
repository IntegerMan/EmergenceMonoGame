using MattEland.Emergence.World;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient.Input;

public class PlayerController(Player player, IWorldService worldService)
{
    public bool Move(Direction direction)
    {
        return worldService.MoveEntity(player, direction);
    }

    public bool Update(KeyboardStateExtended keyState)
    {
        if (keyState.WasKeyPressed(Keys.W) || keyState.WasKeyPressed(Keys.Up))
        {
            return Move(Direction.Up);
        }

        if (keyState.WasKeyPressed(Keys.S) || keyState.WasKeyPressed(Keys.Down))
        {
            return Move(Direction.Down);
        }

        if (keyState.WasKeyPressed(Keys.A) || keyState.WasKeyPressed(Keys.Left))
        {
            return Move(Direction.Left);
        }

        if (keyState.WasKeyPressed(Keys.D) || keyState.WasKeyPressed(Keys.Right))
        {
            return Move(Direction.Right);
        }

        return false;
    }
}

using System;
using MattEland.Emergence.World;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ECS.Systems;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient.ECS.Systems;

public class PlayerControlKeyboardInputSystem(Player player, GameManager gameManager, IWorldService worldService)
    : IUpdateSystem
{
    private bool Move(Direction direction) => worldService.MoveEntity(player, direction);

    public void Initialize(MonoGame.Extended.ECS.World world)
    {
    }

    public void Update(GameTime gameTime)
    {
        KeyboardStateExtended keyState = KeyboardExtended.GetState();

        bool affectedState = false;
        
        if (keyState.WasKeyPressed(Keys.W) || keyState.WasKeyPressed(Keys.Up))
        {
            affectedState = Move(Direction.Up);
        }
        else if (keyState.WasKeyPressed(Keys.S) || keyState.WasKeyPressed(Keys.Down))
        {
            affectedState = Move(Direction.Down);
        }
        else if (keyState.WasKeyPressed(Keys.A) || keyState.WasKeyPressed(Keys.Left))
        {
            affectedState = Move(Direction.Left);
        }
        else if (keyState.WasKeyPressed(Keys.D) || keyState.WasKeyPressed(Keys.Right))
        {
            affectedState = Move(Direction.Right);
        }

        if (affectedState)
        {
            gameManager.StateHasChanged();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.ECS.Components;
using MattEland.Emergence.DesktopClient.ECS.Messages;
using MattEland.Emergence.World;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;
using Entity = DefaultEcs.Entity;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Input;

[With(typeof(PlayerMovementComponent))]
[With(typeof(GameObjectComponent))]
public class PlayerControlKeyboardInputSystem(DefaultEcs.World world) : AEntitySetSystem<float>(world)
{
    private readonly IWorldService _worldService = world.Get<IWorldService>();

    protected override void Update(float totalSeconds, in Entity entity)
    {
        KeyboardStateExtended keyState = KeyboardExtended.GetState();
        GameObject gameObject = entity.Get<GameObjectComponent>().GameObject;

        bool affectedState = false;
        
        if (keyState.WasKeyPressed(Keys.W) || keyState.WasKeyPressed(Keys.Up))
        {
            affectedState = _worldService.MoveEntity(gameObject, Direction.Up);
        }
        else if (keyState.WasKeyPressed(Keys.S) || keyState.WasKeyPressed(Keys.Down))
        {
            affectedState = _worldService.MoveEntity(gameObject, Direction.Down);
        }
        else if (keyState.WasKeyPressed(Keys.A) || keyState.WasKeyPressed(Keys.Left))
        {
            affectedState = _worldService.MoveEntity(gameObject, Direction.Left);
        }
        else if (keyState.WasKeyPressed(Keys.D) || keyState.WasKeyPressed(Keys.Right))
        {
            affectedState = _worldService.MoveEntity(gameObject, Direction.Right);
        }

        if (affectedState)
        {
            world.Publish(new GameStateChangedMessage());
        }
    }
}
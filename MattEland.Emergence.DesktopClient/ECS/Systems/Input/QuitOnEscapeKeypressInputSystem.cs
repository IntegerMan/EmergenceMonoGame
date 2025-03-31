using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.ECS.Messages;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Input;

public class QuitOnEscapeKeypressInputSystem(DefaultEcs.World world) : ISystem<float>
{
    public void Update(float totalSeconds)
    {
        KeyboardStateExtended keyState = KeyboardExtended.GetState();

        if (keyState.WasKeyPressed(Keys.Escape))
        {
            world.Publish(new ExitRequestedMessage());
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool IsEnabled { get; set; } = true;
}
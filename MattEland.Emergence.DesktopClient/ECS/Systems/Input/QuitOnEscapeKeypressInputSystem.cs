using System;
using DefaultEcs.System;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Input;

public class QuitOnEscapeKeypressInputSystem : ISystem<float>
{
    private readonly GameManager _gameManager;

    public QuitOnEscapeKeypressInputSystem(DefaultEcs.World world)
    {
        _gameManager = world.Get<GameManager>();
    }

    public void Update(float totalSeconds)
    {
        KeyboardStateExtended keyState = KeyboardExtended.GetState();

        bool affectedState = false;

        if (keyState.WasKeyPressed(Keys.Escape))
        {
            _gameManager.ExitRequested = true;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public bool IsEnabled { get; set; } = true;
}
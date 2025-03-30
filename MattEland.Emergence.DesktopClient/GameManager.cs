using MattEland.Emergence.DesktopClient.Input;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient;

public class GameManager(IWorldService worldService, Player player, Level level)
{
    private readonly PlayerController _controller = new(player, worldService);

    private const bool ExitRequested = true;
    private const bool KeepRunning = false;
    
    public bool Update(GameTime gameTime)
    {
        KeyboardExtended.Update();
        KeyboardStateExtended keyState = KeyboardExtended.GetState();
        if (_controller.Update(keyState))
        {
            StateHasChanged = true;
        }

        if (keyState.WasKeyPressed(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        {
            return ExitRequested;
        }
        
        if (StateHasChanged)
        {
            VisibleWindow = worldService.GetVisibleObjects(player, level, Viewport);
            StateHasChanged = false;
        }

        return KeepRunning;
    }

    public ViewportData VisibleWindow { get; set; }

    public bool StateHasChanged { get; private set; } = true; // Want to update the first time
    public ViewportDimensions Viewport { get; set; }
}
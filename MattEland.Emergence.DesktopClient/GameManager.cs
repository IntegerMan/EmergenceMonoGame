using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient;

public class GameManager(IWorldService worldService, Player player, Level level)
{ 
    private bool _isVisibleRegionDirty = true;

    public bool ExitRequested { get; set; }
    
    public void Update(GameTime gameTime)
    {
        /*
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
        {
            return ExitRequested;
        }
        */
        
        if (_isVisibleRegionDirty && Viewport is not null)
        {
            VisibleWindow = worldService.GetVisibleObjects(player, level, Viewport);
            _isVisibleRegionDirty = false;
        }
    }

    public ViewportData? VisibleWindow { get; private set; }

    public void StateHasChanged()
    {
        _isVisibleRegionDirty = true;
    }
    public ViewportDimensions? Viewport { get; set; }
}
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient;

public class GameManager(IWorldService worldService)
{ 
    private bool _isVisibleRegionDirty = true;

    public bool ExitRequested { get; set; }
    
    public void Update(GameTime gameTime)
    {
        if (_isVisibleRegionDirty && Viewport is not null)
        {
            VisibleWindow = worldService.GetVisibleObjects(Viewport);
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
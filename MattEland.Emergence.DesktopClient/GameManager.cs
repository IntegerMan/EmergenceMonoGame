using System;
using DefaultEcs.System;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;

namespace MattEland.Emergence.DesktopClient;

public class GameManager(DefaultEcs.World world) : ISystem<float>
{ 
    private bool _isVisibleRegionDirty = true;
    private readonly IWorldService _worldService = world.Get<IWorldService>();

    public bool ExitRequested { get; set; }
    
    public void Update(float totalSeconds)
    {
        if (_isVisibleRegionDirty && Viewport is not null)
        {
            VisibleWindow = _worldService.GetVisibleObjects(Viewport);
            _isVisibleRegionDirty = false;
        }
    }

    public bool IsEnabled { get; set; } = true;

    public ViewportData? VisibleWindow { get; private set; }

    public void StateHasChanged()
    {
        _isVisibleRegionDirty = true;
    }
    public ViewportDimensions? Viewport { get; set; }
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
using System;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient;

public class GraphicsManager(Game game, GraphicsSettings options) : IDisposable
{
    private readonly GraphicsDeviceManager _graphics = new(game);

    private GameWindow Window => game.Window;
    
    public void Maximize()
    {
        // Tell the OS we don't want to change the resolution. This makes the resize performant on Linux
        _graphics.HardwareModeSwitch = false;
        Window.IsBorderless = true;

        // Change the resolution to the current display mode, making the app fullscreen
        DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        _graphics.PreferredBackBufferWidth = displayMode.Width;
        _graphics.PreferredBackBufferHeight = displayMode.Height;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }
    
    public ViewportDimensions CalculateViewport()
    {
        int tileSize = options.TileSize;
        float width = Window.ClientBounds.Width;
        float height = Window.ClientBounds.Height;

        return new ViewportDimensions(
            (int)Math.Ceiling(width / tileSize),
            (int)Math.Ceiling(height / tileSize),
            tileSize);
    }
    
    public void Dispose()
    {
        _graphics?.Dispose();
        
        GC.SuppressFinalize(this);
    }
}
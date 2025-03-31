using System;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace MattEland.Emergence.DesktopClient;

public class GraphicsManager(Game game, GraphicsSettings options) : IDisposable
{
    private RectangleBrush? _rectangleBrush;
    private BitmapFont? _font;
    
    public GameWindow Window => game.Window;
    public GraphicsDevice GraphicsDevice => game.GraphicsDevice;
    public BitmapFont DebugFont => _font ?? throw new InvalidOperationException("Font used before loaded");
    public RectangleBrush Rectangles => _rectangleBrush ?? throw new InvalidOperationException("Rectangle renderer used before loaded");
    public GraphicsSettings Options => options;
    /*
    public void Maximize()
    {
        // Tell the OS we don't want to change the resolution. This makes the resize performant on Linux
        _graphics.HardwareModeSwitch = false;
        Window.IsBorderless = true;
        
        _graphics.SynchronizeWithVerticalRetrace = true;

        // Change the resolution to the current display mode, making the app fullscreen
        DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        _graphics.PreferredBackBufferWidth = displayMode.Width;
        _graphics.PreferredBackBufferHeight = displayMode.Height;
        _graphics.IsFullScreen = true;
        _graphics.ApplyChanges();
    }
    */
    
    public void Dispose()
    {
        _rectangleBrush?.Dispose();
        
        GC.SuppressFinalize(this);
    }

    public void LoadContent()
    {
        _rectangleBrush = new RectangleBrush(GraphicsDevice);
        _font = game.Content.Load<BitmapFont>("fonts/Tahoma");
        
        // Target 60 FPS
        game.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / options.TargetFramesPerSecond);
    }
}
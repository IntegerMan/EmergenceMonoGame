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
    private readonly GraphicsDeviceManager _graphics = new(game);
    private RectangleBrush? _rectangleBrush;
    private SpriteBatch? _spriteBatch;
    private BitmapFont? _font;
    
    public GameWindow Window => game.Window;
    public GraphicsDevice GraphicsDevice => game.GraphicsDevice;
    public BitmapFont DebugFont => _font ?? throw new InvalidOperationException("Font used before loaded");
    public RectangleBrush Rectangles => _rectangleBrush ?? throw new InvalidOperationException("Rectangle renderer used before loaded");
    public GraphicsSettings Options => options;
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
        _graphics.Dispose();
        _rectangleBrush?.Dispose();
        _spriteBatch?.Dispose();
        
        GC.SuppressFinalize(this);
    }

    public void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _rectangleBrush = new RectangleBrush(GraphicsDevice);
        _font = game.Content.Load<BitmapFont>("fonts/Tahoma");
        
        // Target 60 FPS
        game.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / options.TargetFramesPerSecond);
    }
}
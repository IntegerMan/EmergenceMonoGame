using System;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.Renderers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace MattEland.Emergence.DesktopClient;

public class GraphicsManager(Game game, GraphicsSettings options) : IDisposable
{
    private readonly GraphicsDeviceManager _graphics = new(game);
    private RectangleRenderer _rectangleBrush;
    private WorldRenderer _worldRenderer;
    private SpriteBatch _spriteBatch;
    private BitmapFont _font;

    private GameWindow Window => game.Window;
    private GraphicsDevice GraphicsDevice => game.GraphicsDevice;
    private ContentManager Content => game.Content;
    
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
        _rectangleBrush?.Dispose();
        _spriteBatch?.Dispose();
        
        GC.SuppressFinalize(this);
    }

    public void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _rectangleBrush = new RectangleRenderer(GraphicsDevice);
        _worldRenderer = new WorldRenderer(options);
        _font = Content.Load<BitmapFont>("fonts/Tahoma");
    }

    public void Draw(GameTime gameTime, ViewportData visibleWindow)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        
        _worldRenderer.Render(_spriteBatch, _rectangleBrush, visibleWindow);
        _spriteBatch.DrawString(_font, $"FPS: {1f / gameTime.ElapsedGameTime.TotalSeconds:0.0}", new Vector2(10, 10), Color.White);
        
        _spriteBatch.End();
    }
}
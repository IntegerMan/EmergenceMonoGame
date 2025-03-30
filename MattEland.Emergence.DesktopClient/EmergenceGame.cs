using System;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.Input;
using MattEland.Emergence.DesktopClient.Renderers;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly GraphicsDeviceManager _graphics;
    private readonly ILevelGenerator _levelGenerator;
    private readonly IWorldService _worldService;
    private Level _level;
    private Player _player;
    private RectangleRenderer _rectangleBrush;
    private SpriteBatch _spriteBatch;
    private WorldRenderer _worldRenderer;
    private readonly GraphicsSettings _graphicsOptions;
    private GameManager _gameManager;

    public EmergenceGame(IWorldService worldService, ILevelGenerator levelGenerator, IOptionsSnapshot<GraphicsSettings> graphics)
    {
        _graphicsOptions = graphics.Value;
        _worldService = worldService;
        _levelGenerator = levelGenerator;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";

        _graphics = new GraphicsDeviceManager(this);

        // Optionally start the window as maximized
        if (_graphicsOptions.StartFullscreen)
        {
            Maximize();
        }
    }

    private void Maximize()
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

    protected override void Initialize()
    {
        _player = _worldService.CreatePlayer();
        _level = _levelGenerator.Generate(_player);
        _gameManager = new GameManager(_worldService, _player, _level);

        base.Initialize();
    }

    private void CalculateViewport()
    {
        int tileSize = _graphicsOptions.TileSize;
        float width = Window.ClientBounds.Width;
        float height = Window.ClientBounds.Height;
        
        _gameManager.Viewport = new ViewportDimensions(
            (int)Math.Ceiling(width / tileSize),
            (int)Math.Ceiling(height / tileSize),
            tileSize);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        CalculateViewport();
        
        // Load renderers and other content
        _rectangleBrush = new RectangleRenderer(GraphicsDevice);
        _worldRenderer = new WorldRenderer(_graphicsOptions);
    }

    protected override void Update(GameTime gameTime)
    {
        bool exitRequested = _gameManager.Update(gameTime);
        if (exitRequested)
        {
            Exit();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        _spriteBatch.Begin();
        
        _worldRenderer.Render(_spriteBatch, _rectangleBrush, _gameManager.VisibleWindow);
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            // These shouldn't be null at run time, but can be for unit tests of the Dependency Injection container
            _rectangleBrush?.Dispose();
            _spriteBatch?.Dispose();
            
            // Nothing in _worldRenderer to dispose at the moment
        }

        base.Dispose(disposing);
    }
}
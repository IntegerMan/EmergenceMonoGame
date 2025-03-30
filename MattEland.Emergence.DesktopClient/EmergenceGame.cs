using System;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.Renderers;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly ILevelGenerator _levelGenerator;
    private readonly IWorldService _worldService;
    private Level _level;
    private Player _player;
    private RectangleRenderer _rectangleBrush;
    private SpriteBatch _spriteBatch;
    private WorldRenderer _worldRenderer;
    private readonly GraphicsSettings _graphicsOptions;
    private GameManager _gameManager;
    private readonly GraphicsManager _graphicsManager;

    public EmergenceGame(IWorldService worldService, ILevelGenerator levelGenerator, IOptionsSnapshot<GraphicsSettings> graphics)
    {
        _graphicsOptions = graphics.Value;
        _worldService = worldService;
        _levelGenerator = levelGenerator;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";

        _graphicsManager = new GraphicsManager(this, _graphicsOptions);

        // Optionally start the window as maximized
        if (_graphicsOptions.StartFullscreen)
        {
            _graphicsManager.Maximize();
        }
    }

    protected override void Initialize()
    {
        _player = _worldService.CreatePlayer();
        _level = _levelGenerator.Generate(_player);
        _gameManager = new GameManager(_worldService, _player, _level);

        base.Initialize();
    }


    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _gameManager.Viewport = _graphicsManager.CalculateViewport();
        
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
            _graphicsManager?.Dispose();
            _rectangleBrush?.Dispose();
            _spriteBatch?.Dispose();
        }

        base.Dispose(disposing);
    }
}
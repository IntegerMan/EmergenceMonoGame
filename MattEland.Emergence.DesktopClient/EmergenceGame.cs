using System;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.ECS.Systems;
using MattEland.Emergence.DesktopClient.ECS.Systems.Input;
using MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;
using MattEland.Emergence.DesktopClient.Scenes;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Console;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Core
{
    private readonly GraphicsSettings _graphicsSettings;

    private DefaultEcs.World? _world;

    private SpriteBatch? _spriteBatch;

    private readonly IWorldService _worldService;
    
    private ISystem<float>? _renderSystem;
    private ISystem<float>? _updateSystem;
    private RectangleBrush? _rectangles;

    public EmergenceGame(IWorldService worldService, IOptionsSnapshot<GraphicsSettings> graphics)
    {
        _graphicsSettings = graphics.Value;
        /*
        GraphicsSettings graphicsOptions = graphics.Value;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";
        Window.AllowAltF4 = true;
        Window.AllowUserResizing = true;

        // Set up graphics management
        _graphicsManager = new GraphicsManager(this, graphicsOptions);
        if (graphicsOptions.StartFullscreen)
        {
            //_graphicsManager.Maximize();
        }
        */
        worldService.StartWorld();
        _worldService = worldService;
    }
    
    protected override void Initialize()
    {
        base.Initialize();
        
        // Graphical Settings
        DebugConsole.RenderScale = _graphicsSettings.DebugRenderScale;
        Scene.SetDefaultDesignResolution(_graphicsSettings.DesignWidth, _graphicsSettings.DesignHeight, Scene.SceneResolutionPolicy.NoBorderPixelPerfect);
        Screen.SetSize(_graphicsSettings.DesignWidth, _graphicsSettings.DesignHeight);
        Window.AllowUserResizing = true;
        
        // Entity Component System and render batching
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _rectangles = new RectangleBrush(GraphicsDevice);
        InitializeEntityComponentSystem();
        
        // Scene management
        Scene = new GameScene();
    }

    private void InitializeEntityComponentSystem()
    {
        if (_spriteBatch is null) throw new InvalidOperationException("SpriteBatch not initialized");

        // Set up the Entity Component System World
        _world = new DefaultEcs.World();
        _world.Set(_worldService);
        _world.Set(_graphicsSettings);
        _world.Set(_worldService.Player);
        _world.Set(_worldService.Level);
        _world.Set(_worldService.Level);
        _world.Set(_rectangles);
        _world.Set(Content);

        GameManager gameManager = new(_world);
        gameManager.Viewport = CalculateViewport();
        _world.Set(gameManager);
        
        // Update Systems are invoked during the Update phase and can potentially be run in parallel
        _updateSystem = new SequentialSystem<float>(
            gameManager,
            new LevelManagementSystem(_world),
            new PlayerControlKeyboardInputSystem(_world),
            new QuitOnEscapeKeypressInputSystem(_world)
        );

        // Render Systems are invoked from bottom of the Z-order to top during the Draw phase and share a sprite batch
        _renderSystem = new SequentialSystem<float>(
            new WorldRenderer(_world, _spriteBatch),
            new GameObjectRenderer(_world, _spriteBatch)//,
            //new VersionNumberRenderer(_world, _spriteBatch),
            //new FramesPerSecondRenderer(_world, _spriteBatch)
        );
    }
    
    public ViewportDimensions CalculateViewport()
    {
        int tileSize = _graphicsSettings.TileSize;
        float width = Window.ClientBounds.Width;
        float height = Window.ClientBounds.Height;

        return new ViewportDimensions(
            (int)Math.Ceiling(width / tileSize),
            (int)Math.Ceiling(height / tileSize),
            tileSize);
    }


    protected override void Update(GameTime gameTime)
    {
        _updateSystem!.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        _spriteBatch!.Begin();
        _renderSystem!.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        _spriteBatch.End();
        
        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            //_graphicsManager.Dispose();
            _renderSystem?.Dispose();
            _updateSystem?.Dispose();
            _spriteBatch?.Dispose();
            _rectangles?.Dispose();
            _world?.Dispose();
        }

        base.Dispose(disposing);
    }
}
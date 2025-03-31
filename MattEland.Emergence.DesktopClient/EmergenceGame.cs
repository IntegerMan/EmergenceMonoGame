using System;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.ECS.Systems;
using MattEland.Emergence.DesktopClient.ECS.Systems.Input;
using MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly GameManager _gameManager;
    private readonly GraphicsManager _graphicsManager;
    private ISystem<float>? _renderSystem;
    private ISystem<float>? _updateSystem;
    private readonly DefaultEcs.World _world;
    private SpriteBatch? _spriteBatch;

    public EmergenceGame(IWorldService worldService, ILevelGenerator levelGenerator,
        IOptionsSnapshot<GraphicsSettings> graphics)
    {
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
            _graphicsManager.Maximize();
        }

        (Level level, Player player) = worldService.StartWorld();
        _gameManager = new GameManager(worldService);
        
        // Set up the Entity Component System World
        _world = new DefaultEcs.World();
        _world.Set(worldService);
        _world.Set(graphicsOptions);
        _world.Set(_gameManager);
        _world.Set(_graphicsManager);
        _world.Set(player);
        _world.Set(level);
        _world.Set(Content);
    }

    private void InitializeEntityComponentSystem()
    {
        if (_spriteBatch is null) throw new InvalidOperationException("SpriteBatch not initialized");
        
        _updateSystem = new SequentialSystem<float>(
            new LevelManagementSystem(_world),
            new PlayerControlKeyboardInputSystem(_world),
            new QuitOnEscapeKeypressInputSystem(_world)
        );
        _renderSystem = new SequentialSystem<float>(
            new WorldRenderer(_world, _spriteBatch),
            new GameObjectRenderer(_world, _spriteBatch),
            new VersionNumberRenderer(_world, _spriteBatch),
            new FramesPerSecondRenderer(_world, _spriteBatch)
        );
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        InitializeEntityComponentSystem();

        _gameManager.Viewport = _graphicsManager.CalculateViewport();

        // Load renderers and other content
        _graphicsManager.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardExtended.Update();
        _updateSystem!.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        _gameManager.Update(gameTime);
        if (_gameManager.ExitRequested)
        {
            Exit();
        }

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
            _graphicsManager.Dispose();
            _renderSystem?.Dispose();
            _updateSystem?.Dispose();
            _spriteBatch?.Dispose();
            _world.Dispose();
        }

        base.Dispose(disposing);
    }
}
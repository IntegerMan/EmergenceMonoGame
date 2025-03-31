using DefaultEcs.System;
using DefaultEcs.Threading;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.ECS.Systems;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly IWorldService _worldService;
    private readonly GameManager _gameManager;
    private readonly GraphicsManager _graphicsManager;
    private readonly Player _player;
    private ISystem<float>? _renderSystem;
    private ISystem<float>? _updateSystem;

    public EmergenceGame(IWorldService worldService, ILevelGenerator levelGenerator,
        IOptionsSnapshot<GraphicsSettings> graphics)
    {
        _worldService = worldService;
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

        _player = worldService.CreatePlayer();
        Level level = levelGenerator.Generate(_player);
        _gameManager = new GameManager(worldService, _player, level);
    }

    protected override void Initialize()
    {
        DefaultEcs.World world = new();
        world.Set(Content);
        world.Set(_graphicsManager);
        world.Set(_gameManager);

        _updateSystem = new SequentialSystem<float>(
            //new PlayerControlKeyboardInputSystem(_player, _gameManager, _worldService),
        );
        _renderSystem = new SequentialSystem<float>(
            //new WorldRenderer(_gameManager, _graphicsManager),
            new VersionNumberRenderer(world),
            new FramesPerSecondRenderer(world)
        );

        base.Initialize();
    }

    /*
    private IGameComponent ConfigureEntityComponentSystem()
    {
        DefaultEcs.World world = new DefaultEcs.World();

        // Set up the ECS world with the necessary systems


        return new WorldBuilder()
            .AddSystem(new WorldRenderer(_gameManager, _graphicsManager))
            .AddSystem(new FramesPerSecondRenderer(_graphicsManager))
            .AddSystem(new VersionNumberRenderer(_graphicsManager))
            .AddSystem(new PlayerControlKeyboardInputSystem(_player, _gameManager, _worldService))
            .Build();
    }
    */

    protected override void LoadContent()
    {
        _gameManager.Viewport = _graphicsManager.CalculateViewport();

        // Load renderers and other content
        _graphicsManager.LoadContent();
        base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
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
        _renderSystem!.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        base.Draw(gameTime);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _graphicsManager.Dispose();
        }

        base.Dispose(disposing);
    }
}
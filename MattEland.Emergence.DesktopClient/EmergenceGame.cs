using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.ECS.Systems;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly ILevelGenerator _levelGenerator;
    private readonly IWorldService _worldService;
    private Level? _level;
    private Player? _player;
    private GameManager? _gameManager;
    private readonly GraphicsManager _graphicsManager;

    public EmergenceGame(IWorldService worldService, ILevelGenerator levelGenerator, IOptionsSnapshot<GraphicsSettings> graphics)
    {
        GraphicsSettings graphicsOptions = graphics.Value;
        _worldService = worldService;
        _levelGenerator = levelGenerator;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";

        // Set up graphics management
        _graphicsManager = new GraphicsManager(this, graphicsOptions);
        if (graphicsOptions.StartFullscreen)
        {
            _graphicsManager.Maximize();
        }
    }

    protected override void Initialize()
    {
        _player = _worldService.CreatePlayer();
        _level = _levelGenerator.Generate(_player);
        _gameManager = new GameManager(_worldService, _player, _level);

        // Configure our Entity Component System (ECS)
        Components.Add(ConfigureEntityComponentSystem());

        base.Initialize();
    }

    private MonoGame.Extended.ECS.World ConfigureEntityComponentSystem() 
        => new WorldBuilder()
            .AddSystem(new WorldRenderer(_gameManager, _graphicsManager))
            .AddSystem(new FramesPerSecondRenderer(_graphicsManager))
            .AddSystem(new VersionNumberRenderer(_graphicsManager))
            .Build();

    protected override void LoadContent()
    {
        _gameManager!.Viewport = _graphicsManager.CalculateViewport();
        
        // Load renderers and other content
        _graphicsManager.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        bool exitRequested = _gameManager!.Update(gameTime);
        if (exitRequested)
        {
            Exit();
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
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
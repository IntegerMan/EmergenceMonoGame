using System;
using DefaultEcs.System;
using DefaultEcs.Threading;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.ECS.Messages;
using MattEland.Emergence.DesktopClient.ECS.Systems;
using MattEland.Emergence.DesktopClient.ECS.Systems.Input;
using MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;
using Microsoft.Extensions.Options;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.Collections;
using MonoGame.Extended.Input;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly GameManager _gameManager;
    private readonly GraphicsDeviceManager _graphicsManager;
    
    private ParallelSystem<float>? _updateSystem;
    private SequentialSystem<float>? _renderSystem;
    
    private DefaultEcs.World? _world;
    private SpriteBatch? _spriteBatch;
    private RectangleBrush? _rectangleBrush;
    private BitmapFont? _font;
    
    private readonly GraphicsSettings _graphicsOptions;
    private readonly IWorldService _worldService;
    private readonly Bag<IDisposable> _eventSubscriptions = new();

    public EmergenceGame(IWorldService worldService, IOptionsSnapshot<GraphicsSettings> graphics)
    {
        _graphicsOptions = graphics.Value;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";
        Window.AllowAltF4 = true;
        Window.AllowUserResizing = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1.0 / _graphicsOptions.TargetFramesPerSecond);
        
        // Set up graphics management
        _graphicsManager = new GraphicsDeviceManager(this);
        _graphicsManager.SynchronizeWithVerticalRetrace = true; // Turn on VSync because it helps with performance and reduces screen tearing
        if (_graphicsOptions.StartFullscreen)
        {
            Maximize();
        }
        _graphicsManager.ApplyChanges();
        
        _worldService = worldService;
        _gameManager = new GameManager(worldService);
    }

    private void InitializeEntityComponentSystem()
    {
        if (_spriteBatch is null) throw new InvalidOperationException("SpriteBatch not initialized");
        
        // Set up the Entity Component System World
        _world = new DefaultEcs.World();
        _world.Set(_worldService);
        _world.Set(_graphicsOptions);
        _world.Set(_gameManager);
        _world.Set(_graphicsManager);
        _world.Set(_worldService.Player);
        _world.Set(_worldService.Level);
        _world.Set(_rectangleBrush);
        _world.Set(_font);
        _world.Set(Content);
        _world.Set(Window);
        
        // Update Systems are invoked during the Update phase and can potentially be run in parallel
        _updateSystem = new ParallelSystem<float>(
            new DefaultParallelRunner(degreeOfParallelism: 3),
            new LevelManagementSystem(_world),
            new PlayerControlKeyboardInputSystem(_world),
            new QuitOnEscapeKeypressInputSystem(_world)
        );
        
        // Render Systems are invoked from bottom of the Z-order to top during the Draw phase and share a sprite batch
        _renderSystem = new SequentialSystem<float>(
            new WorldRenderer(_world, _spriteBatch),
            new GameObjectRenderer(_world, _spriteBatch),
            new VersionNumberRenderer(_world, _spriteBatch),
            new FramesPerSecondRenderer(_world, _spriteBatch)
        );
        
        // Subscribe to top-level messages we care about
        _eventSubscriptions.AddRange([
            _world.Subscribe<ExitRequestedMessage>(OnExitRequested),
            _world.Subscribe<GameStateChangedMessage>(OnGameStateChanged)
        ]);
    }

    private void OnGameStateChanged(in GameStateChangedMessage message)
    {
        _gameManager.StateHasChanged();
    }

    private void OnExitRequested(in ExitRequestedMessage message) => Exit();

    protected override void Initialize()
    {
        _worldService.StartWorld();
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _rectangleBrush = new RectangleBrush(GraphicsDevice);
        _font = Content.Load<BitmapFont>("fonts/Tahoma");
        InitializeEntityComponentSystem();

        // Set the initial contents of the viewport
        _gameManager.Viewport = CalculateViewport();
        _gameManager.Update();

        base.LoadContent();
    }

    private ViewportDimensions CalculateViewport()
    {
        int tileSize = _graphicsOptions.TileSize;
        float width = Window.ClientBounds.Width;
        float height = Window.ClientBounds.Height;

        return new ViewportDimensions(
            (int)Math.Ceiling(width / tileSize),
            (int)Math.Ceiling(height / tileSize),
            tileSize);
    }

    protected override void Update(GameTime gameTime)
    {
        KeyboardExtended.Update();
        _updateSystem!.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

        _gameManager.Update();
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
            _rectangleBrush?.Dispose();
            _world?.Dispose();
            foreach (IDisposable subscription in _eventSubscriptions)
            {
                subscription.Dispose();
            }
        }

        base.Dispose(disposing);
    }

    private void Maximize()
    {
        // Tell the OS we don't want to change the resolution. This makes the resize performant on Linux
        _graphicsManager.HardwareModeSwitch = false;
        Window.IsBorderless = true;
        
        _graphicsManager.SynchronizeWithVerticalRetrace = true;

        // Change the resolution to the current display mode, making the app fullscreen
        DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        _graphicsManager.PreferredBackBufferWidth = displayMode.Width;
        _graphicsManager.PreferredBackBufferHeight = displayMode.Height;
        _graphicsManager.IsFullScreen = true;

    }
}
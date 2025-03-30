using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Extensions.Options;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    public const int TileSize = 32;
    private readonly GraphicsDeviceManager _graphics;
    private readonly IWorldService _worldService;
    private Texture2D _blankTexture;
    private Level _level;
    private Player _player;
    private SpriteBatch _spriteBatch;
    private bool _stateHasChanged = true;
    private ViewportDimensions _viewportDimensions;
    private ViewportData _visibleWindow;

    public EmergenceGame(IWorldService worldService, IOptionsSnapshot<GraphicsSettings> appSettings)
    {
        _worldService = worldService;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";

        _graphics = new GraphicsDeviceManager(this);

        // Optionally start the window as maximized
        if (appSettings.Value.StartFullscreen)
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
        _level = _worldService.CreateLevel(_player);
        _viewportDimensions = new ViewportDimensions(11, 7);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // use this.Content to load your game content here
        _blankTexture = new Texture2D(GraphicsDevice, 1, 1);
        _blankTexture.SetData([Color.White]);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Get any view information here
        if (_stateHasChanged)
        {
            _visibleWindow = _worldService.GetVisibleObjects(_player, _level, _viewportDimensions);
            _stateHasChanged = false;
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        foreach (TileInfo tile in _visibleWindow.VisibleTiles)
        {
            Rectangle tileRect = new Rectangle(tile.Pos.X * TileSize, tile.Pos.Y * TileSize, TileSize, TileSize);
            _spriteBatch.Draw(_blankTexture, tileRect, tile.GetTileColor());
        }

        foreach (GameObject obj in _visibleWindow.VisibleObjects)
        {
            // Draw object
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
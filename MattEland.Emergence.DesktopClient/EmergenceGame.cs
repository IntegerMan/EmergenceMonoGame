using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MattEland.Emergence.DesktopClient;

public class EmergenceGame : Game
{
    private readonly IWorldService _worldService;
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private ViewportDimensions _viewportDimensions;
    private Player _player;
    private Level _level;
    private ViewportData _visibleWindow;
    private Texture2D _blankTexture;
    private bool _stateHasChanged = true;

    public EmergenceGame(IWorldService worldService)
    {
        _worldService = worldService;
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";

        // Start the window as maximized
        DisplayMode displayMode = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode;
        _graphics = new GraphicsDeviceManager(this)
        {
            PreferredBackBufferWidth = displayMode.Width,
            PreferredBackBufferHeight = displayMode.Height,
            IsFullScreen = true
        };
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

    public const int TileSize = 32;

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
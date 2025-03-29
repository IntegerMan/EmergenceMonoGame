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

    public EmergenceGame(IWorldService worldService)
    {
        _worldService = worldService;
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Emergence";
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

        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Only get visible if the game state has changed
        _visibleWindow = _worldService.GetVisibleObjects(_player, _level, _viewportDimensions);

        base.Update(gameTime);
    }

    public const int TileSize = 32;

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        if (_blankTexture == null)
        {
            _blankTexture = new Texture2D(GraphicsDevice, 1, 1);
            _blankTexture.SetData([Color.White]);
        }
        
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
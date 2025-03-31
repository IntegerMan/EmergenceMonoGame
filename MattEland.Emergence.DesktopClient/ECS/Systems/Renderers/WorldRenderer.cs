using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.Helpers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;

public class WorldRenderer : ISystem<float>
{
    private readonly SpriteBatch _spriteBatch;
    private readonly GraphicsManager _graphicsManager;
    private readonly GameManager _gameManager;

    public WorldRenderer(DefaultEcs.World world)
    {
        _graphicsManager = world.Get<GraphicsManager>();
        _gameManager = world.Get<GameManager>();
        _spriteBatch = new SpriteBatch(_graphicsManager.GraphicsDevice);
    }

    public void Update(float totalSeconds)
    {
        int tileSize = _graphicsManager.Options.TileSize;
        ViewportData visibleWindow = _gameManager.VisibleWindow ?? throw new InvalidOperationException("Visible window not set");
        Point offset = visibleWindow.UpperLeft.ToOffset(tileSize);

        RectangleBrush rectangles = _graphicsManager.Rectangles;
        _spriteBatch.Begin();

        // Render floors
        foreach (TileInfo tile in visibleWindow.VisibleTiles)
        {
            rectangles.Render(tile.Pos.ToRectangle(tileSize, offset), tile.GetTileColor(), _spriteBatch);
        }

        // Render objects
        foreach (GameObject obj in visibleWindow.VisibleObjects)
        {
            rectangles.Render(obj.Pos.ToRectangle(tileSize, offset), Color.Yellow, _spriteBatch);
        }

        _spriteBatch.End();
    }

    public bool IsEnabled { get; set; } = true;

    public void Dispose()
    {
        _spriteBatch.Dispose();
        GC.SuppressFinalize(this);
    }
}
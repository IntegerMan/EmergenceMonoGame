using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.Helpers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;

public class WorldRenderer(DefaultEcs.World world, SpriteBatch spriteBatch) : ISystem<float>
{
    private readonly GraphicsManager _graphicsManager = world.Get<GraphicsManager>();
    private readonly GameManager _gameManager = world.Get<GameManager>();

    public void Update(float totalSeconds)
    {
        int tileSize = _graphicsManager.Options.TileSize;
        ViewportData visibleWindow = _gameManager.VisibleWindow ?? throw new InvalidOperationException("Visible window not set");
        Point offset = visibleWindow.UpperLeft.ToOffset(tileSize);

        RectangleBrush rectangles = _graphicsManager.Rectangles;

        // Render floors
        foreach (TileInfo tile in visibleWindow.VisibleTiles)
        {
            rectangles.Render(tile.Pos.ToRectangle(tileSize, offset), tile.GetTileColor(), spriteBatch);
        }
    }

    public bool IsEnabled { get; set; } = true;
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
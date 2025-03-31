using System;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.Helpers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ECS.Systems;

namespace MattEland.Emergence.DesktopClient.ECS.Systems;

public class WorldRenderer(GameManager gameManager, GraphicsManager graphicsManager) : IDrawSystem
{
    private readonly SpriteBatch _spriteBatch = new(graphicsManager.GraphicsDevice);

    public void Initialize(MonoGame.Extended.ECS.World world)
    {
    }

    public void Draw(GameTime gameTime)
    {
        int tileSize = graphicsManager.Options.TileSize;
        ViewportData visibleWindow =
            gameManager.VisibleWindow ?? throw new InvalidOperationException("Visible window not set");
        Point offset = visibleWindow.UpperLeft.ToOffset(tileSize);

        RectangleBrush rectangles = graphicsManager.Rectangles;
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

    public void Dispose()
    {
        _spriteBatch.Dispose();
        GC.SuppressFinalize(this);
    }
}
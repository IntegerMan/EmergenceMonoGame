using System;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.Helpers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient.Renderers;

public class WorldRenderer(GraphicsSettings graphicsSettings)
{
    public void Render(SpriteBatch spriteBatch, RectangleRenderer rectangleRenderer, ViewportData visibleWindow)
    {
        int tileSize = graphicsSettings.TileSize;
        
        // Render viewport (for debugging)
        ViewportDimensions viewport = visibleWindow.Viewport;
        Point size = new(viewport.Width * viewport.TileSize, viewport.Height * viewport.TileSize);
        Point upperLeft = visibleWindow.UpperLeft.ToPoint(tileSize);
        Rectangle bounds = new(upperLeft, size);
        rectangleRenderer.Render(bounds, Color.CornflowerBlue, spriteBatch);
        
        // Render floors
        foreach (TileInfo tile in visibleWindow.VisibleTiles)
        {
            rectangleRenderer.Render(tile.Pos.ToRectangle(tileSize), tile.GetTileColor(), spriteBatch);
        }

        // Render objects
        foreach (GameObject obj in visibleWindow.VisibleObjects)
        {
            rectangleRenderer.Render(obj.Pos.ToRectangle(tileSize), Color.Yellow, spriteBatch);
        }
    }
}
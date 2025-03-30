using System;
using MattEland.Emergence.DesktopClient.Configuration;
using MattEland.Emergence.DesktopClient.Helpers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient.Renderers;

public class WorldRenderer(GraphicsSettings graphicsSettings)
{
    private readonly Point _origin = new(0,0);

    public void Render(SpriteBatch batch, RectangleRenderer rectangleRenderer, ViewportData visibleWindow)
    {
        ArgumentNullException.ThrowIfNull(visibleWindow);
        
        int tileSize = graphicsSettings.TileSize;
        Point offset = visibleWindow.UpperLeft.ToOffset(tileSize);
        
        // Debugging option to help find render area and render issues
        if (graphicsSettings.DebugViewport)
        {
            RenderViewportBounds(batch, rectangleRenderer, visibleWindow);
        }

        // Render floors
        foreach (TileInfo tile in visibleWindow.VisibleTiles)
        {
            rectangleRenderer.Render(tile.Pos.ToRectangle(tileSize, offset), tile.GetTileColor(), batch);
        }

        // Render objects
        foreach (GameObject obj in visibleWindow.VisibleObjects)
        {
            rectangleRenderer.Render(obj.Pos.ToRectangle(tileSize, offset), Color.Yellow, batch);
        }
    }

    private void RenderViewportBounds(SpriteBatch batch, RectangleRenderer rectangleRenderer, ViewportData viewData)
    {
        ViewportDimensions viewport = viewData.Viewport;
        Point size = new(viewport.Width * viewport.TileSize, viewport.Height * viewport.TileSize);
        Rectangle bounds = new(_origin, size);
        rectangleRenderer.Render(bounds, Color.CornflowerBlue, batch);
    }
}
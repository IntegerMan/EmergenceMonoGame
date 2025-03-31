using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.Brushes;
using MattEland.Emergence.DesktopClient.ECS.Components;
using MattEland.Emergence.DesktopClient.Helpers;
using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework.Graphics;
using Entity = DefaultEcs.Entity;
using Point = Microsoft.Xna.Framework.Point;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;

[With(typeof(GameObjectComponent))]
public class GameObjectRenderer(DefaultEcs.World world, SpriteBatch spriteBatch) : AEntitySetSystem<float>(world)
{
    private readonly GraphicsManager _graphicsManager = world.Get<GraphicsManager>();
    private readonly GameManager _gameManager = world.Get<GameManager>();

    protected override void Update(float state, ReadOnlySpan<Entity> entities)
    {
        RectangleBrush rectangles = _graphicsManager.Rectangles;
        int tileSize = _graphicsManager.Options.TileSize;
        ViewportData visibleWindow = _gameManager.VisibleWindow ?? throw new InvalidOperationException("Visible window not set");
        Point offset = visibleWindow.UpperLeft.ToOffset(tileSize);
     
        foreach (var entity in entities)
        {
            GameObjectComponent obj = entity.Get<GameObjectComponent>();
            GameObject gameObject = obj.GameObject;
            rectangles.Render(gameObject.Pos.ToRectangle(tileSize, offset), obj.RenderColor, spriteBatch);
        }
    }
}
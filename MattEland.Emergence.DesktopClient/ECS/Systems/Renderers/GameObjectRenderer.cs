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

[With(typeof(RectangleRendererComponent))]
[With(typeof(WorldPositionComponent))]
public class GameObjectRenderer : AEntitySetSystem<float>
{
    private readonly GraphicsManager _graphicsManager;
    private readonly GameManager _gameManager;
    private readonly SpriteBatch _spriteBatch;

    public GameObjectRenderer(DefaultEcs.World world) : base(world)
    {
        _graphicsManager = world.Get<GraphicsManager>();
        _gameManager = world.Get<GameManager>();
        _spriteBatch = new SpriteBatch(_graphicsManager.GraphicsDevice);
    }

    protected override void Update(float state, ReadOnlySpan<Entity> entities)
    {
        RectangleBrush rectangles = _graphicsManager.Rectangles;
        int tileSize = _graphicsManager.Options.TileSize;
        ViewportData visibleWindow = _gameManager.VisibleWindow ?? throw new InvalidOperationException("Visible window not set");
        Point offset = visibleWindow.UpperLeft.ToOffset(tileSize);
     
        _spriteBatch.Begin();
        foreach (var entity in entities)
        {
            RectangleRendererComponent rectangle = entity.Get<RectangleRendererComponent>();
            WorldPositionComponent pos = entity.Get<WorldPositionComponent>();

            rectangles.Render(pos.Pos.ToRectangle(tileSize, offset), rectangle.Color, _spriteBatch);
        }
        _spriteBatch.End();
    }

    public override void Dispose()
    {
        _spriteBatch.Dispose();
        
        base.Dispose();
    }
}
using MattEland.Emergence.World.Entities;
using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Services;

public class WorldService(ILevelGenerator levelGenerator) : IWorldService
{
    private Player? _player;
    
    public bool MoveEntity(GameObject entity, Direction direction)
    {
        // This should eventually need to worry about collisions, bump combat, level geometry, etc.
        entity.Pos = entity.Pos.Offset(direction);

        return true; // false if invalid
    }
    
    public ViewportData GetVisibleObjects(ViewportDimensions viewport)
    {
        if (Level is null) throw new InvalidOperationException("No level loaded");
        
        // We need an origin we're viewing from. Set that to the player's position
        Pos2D pos = Player.Pos;
        
        // Figure out upper left and lower right corners of the viewport
        int halfViewportWidth = viewport.Width / 2;
        int halfViewportHeight = viewport.Height / 2;
        Pos2D upperLeft = new(pos.X - halfViewportWidth, pos.Y - halfViewportHeight);
        Pos2D lowerRight = upperLeft.Offset(viewport.Width - 1, viewport.Height - 1);
        
        return new ViewportData
        {
            Viewport = viewport,
            Center = pos,
            VisibleTiles = PopulateVisibleTiles(Level, viewport, upperLeft, lowerRight),
            VisibleObjects = Level.Objects.Where(obj => IsInViewport(obj.Pos, upperLeft, lowerRight))
        };
    }

    public Player Player => _player ??= new Player(new Pos2D(0, 0));
    
    public Level? Level { get; private set; }
  
    public (Level level, Player player) StartWorld()
    {
        _player = new Player(new Pos2D(0, 0));
        Level = levelGenerator.Generate(_player);
        
        return (Level, _player);
    }

    private static List<TileInfo> PopulateVisibleTiles(Level level, ViewportDimensions viewport, Pos2D upperLeft, Pos2D lowerRight)
    {
        List<TileInfo> visibleTiles = new(viewport.Width * viewport.Height);

        for (int y = upperLeft.Y; y <= lowerRight.Y; y++)
        {
            for (int x = upperLeft.X; x <= lowerRight.X; x++)
            {
                Pos2D pos = new(x, y);
                visibleTiles.Add(new TileInfo
                {
                    Pos = pos,
                    Floor = level.GetFloorInfo(pos)
                });
            }
        }

        return visibleTiles;
    }

    private static bool IsInViewport(Pos2D pos, Pos2D upperLeft, Pos2D lowerRight) =>
        pos.X >= upperLeft.X && pos.X <= lowerRight.X &&
        pos.Y >= upperLeft.Y && pos.Y <= lowerRight.Y;
}
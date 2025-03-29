using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Services;

public interface IWorldService
{
    ViewportData GetVisibleObjects(Player perspective, Level level, ViewportDimensions viewport);
    Player CreatePlayer();
    Level CreateLevel(Player player);
}

public class WorldService : IWorldService
{
    public ViewportData GetVisibleObjects(Player perspective, Level level, ViewportDimensions viewport)
    {
        // Figure out upper left and lower right corners of the viewport
        int halfViewportWidth = viewport.Width / 2;
        int halfViewportHeight = viewport.Height / 2;
        WorldPos upperLeft = new WorldPos(perspective.Pos.X - halfViewportWidth, perspective.Pos.Y - halfViewportHeight);
        WorldPos lowerRight = upperLeft.Offset(viewport.Width - 1, viewport.Height - 1);

        return new ViewportData
        {
            Viewport = viewport,
            Center = perspective.Pos,
            VisibleTiles = PopulateVisibleTiles(level, viewport, upperLeft, lowerRight),
            VisibleObjects = level.Objects.Where(obj => IsInViewport(obj.Pos, upperLeft, lowerRight))
        };
    }


    public Player CreatePlayer()
    {
        return new Player(new WorldPos(0, 0));
    }

    public Level CreateLevel(Player player)
    {
        Level level = new Level();
        player.Pos = new WorldPos(2, 3);
        level.AddObject(player);
        
        return level;
    }

    private static List<TileInfo> PopulateVisibleTiles(Level level, ViewportDimensions viewport, WorldPos upperLeft,
        WorldPos lowerRight)
    {
        List<TileInfo> visibleTiles = new(viewport.Width * viewport.Height);
        
        for (int y = upperLeft.Y; y <= lowerRight.Y; y++)
        {
            for (int x = upperLeft.X; x <= lowerRight.X; x++)
            {
                WorldPos pos = new WorldPos(x, y);
                visibleTiles.Add(new TileInfo
                {
                    Pos = pos,
                    Floor = level.GetFloorInfo(pos)
                });
            }
        }

        return visibleTiles;
    }

    private static bool IsInViewport(WorldPos pos, WorldPos upperLeft, WorldPos lowerRight) =>
        pos.X >= upperLeft.X && pos.X <= lowerRight.X &&
        pos.Y >= upperLeft.Y && pos.Y <= lowerRight.Y;
}
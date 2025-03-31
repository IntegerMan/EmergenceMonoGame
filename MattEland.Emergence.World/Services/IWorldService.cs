using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Services;

public interface IWorldService
{
    ViewportData GetVisibleObjects(ViewportDimensions viewport);
    bool MoveEntity(GameObject entity, Direction direction);
    Player Player { get; }
    Level? Level { get; }
    (Level level, Player player) StartWorld();
}
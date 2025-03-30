using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Services;

public interface IWorldService
{
    ViewportData GetVisibleObjects(Player perspective, Level level, ViewportDimensions viewport);
    Player CreatePlayer();
}
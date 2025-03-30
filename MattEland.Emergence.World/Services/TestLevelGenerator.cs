using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Services;

public class TestLevelGenerator : ILevelGenerator
{
    public Level Generate(Player player)
    {
        Level level = new(30, 30);
        player.Pos = new WorldPos(2, 3);
        level.AddObject(player);

        return level;
    }
}
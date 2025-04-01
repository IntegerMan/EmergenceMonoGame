using MattEland.Emergence.World.Entities;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;

namespace MattEland.Emergence.LevelGeneration;

public class TestLevelLevelGenerator : ILevelGenerator
{
    public Level Generate(Player player)
    {
        const string data = """
                            #####
                            #_._#
                            .....
                            #_._#
                            #####
                            """;

        Level level = Level.FromData(data);
        player.Pos = new Pos2D(2, 2);
        level.AddObject(player);

        return level;
    }
}
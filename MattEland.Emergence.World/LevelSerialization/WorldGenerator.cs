using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.LevelSerialization;

public static class WorldGenerator
{
    private static RoomPlacement PlacePrefab(LevelInstruction instr, RoomDataProvider roomProvider)
        => new RoomPlacement(roomProvider.GetRoomById(instr.PrefabId), new Pos2D(instr.X, instr.Y));

    private static Actor GetOrCreatePlayer(Actor? existingPlayer, LevelData levelData)
    {
        if (existingPlayer == null) return new Actor(levelData.PlayerStart, ActorType.Player);

        existingPlayer.Pos = levelData.PlayerStart;
        return existingPlayer;
    }

    public static WorldGenerationResult GenerateMap(int levelId, Actor existingPlayer)
    {
        var roomProvider = new RoomDataProvider();
        var json = string.Empty; // TODO: Not this!
        var levelData = LevelData.LoadFromJson(json);

        var placements = levelData.Instructions.Select(i => PlacePrefab(i, roomProvider)).ToList();

        var player = GetOrCreatePlayer(existingPlayer, levelData);

        List<GameObject> objects = [player];

        // TODO: Analyze instructions to determine min/max x/y
        for (int y = -200; y < 200; y++)
        {
            for (int x = -200; x < 200; x++)
            {
                var pos = new Pos2D(x, y);

                char c = ' ';
                foreach (var p in placements)
                {
                    c = p.GetChar(pos, c);
                }

                if (c != ' ')
                {
                    objects.Add(LevelObjectCreator.GetObject(c, pos));
                }
            }
        }

        return new WorldGenerationResult(levelData, objects);
    }
}
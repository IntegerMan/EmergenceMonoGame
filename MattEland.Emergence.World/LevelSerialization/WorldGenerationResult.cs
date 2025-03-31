using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.LevelSerialization;

public class WorldGenerationResult(LevelData levelData, IEnumerable<GameObject> objects)
{
    private readonly LevelData _levelData = levelData;
    public IEnumerable<GameObject> Objects { get; } = objects;

    public string Name => _levelData.Name;

    public Pos2D PlayerStart => _levelData.PlayerStart;
}
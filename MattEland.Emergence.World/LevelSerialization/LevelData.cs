using MattEland.Emergence.World.Models;
using Newtonsoft.Json;

namespace MattEland.Emergence.World.LevelSerialization;

public class LevelData(string name, Pos2D start, IEnumerable<LevelInstruction> instructions)
{
    public string Name { get; } = name;
    public IEnumerable<LevelInstruction> Instructions { get; } = instructions;
    public Pos2D PlayerStart { get; } = start;

    public static LevelData LoadFromJson(string json) => JsonConvert.DeserializeObject<LevelData>(json)!;
}
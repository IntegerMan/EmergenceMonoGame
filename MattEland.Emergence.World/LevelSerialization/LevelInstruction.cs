namespace MattEland.Emergence.World.LevelSerialization;

public class LevelInstruction(string prefabId, int x, int y, string encounterSet)
{
    public string PrefabId { get; } = prefabId;
    public int X { get; } = x;
    public int Y { get; } = y;
    public string EncounterSet { get; } = encounterSet;
}
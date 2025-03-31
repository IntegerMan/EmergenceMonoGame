using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.LevelSerialization;

public class RoomData
{
    public required string Id { get; init; }
    public required List<string?> Data { get; init; }
    public bool IsInvulnerable { get; set; }

    public char GetCharacterAtPosition(Pos2D pos)
    {
        if (pos.X < 0 || pos.Y < 0 || pos.Y >= Data.Count) return ' ';

        string? row = Data[pos.Y];

        if (row == null || pos.X >= row.Length) return ' ';

        return row[pos.X];
    }
}
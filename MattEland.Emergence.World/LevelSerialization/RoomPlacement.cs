using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.LevelSerialization;

public class RoomPlacement
{
    private readonly RoomData _room;
    private readonly Pos2D _upperLeftCorner;

    public RoomPlacement(RoomData? room, Pos2D upperLeftCorner)
    {
        ArgumentNullException.ThrowIfNull(room);
        
        _room = room;
        _upperLeftCorner = upperLeftCorner;
    }

    public char GetChar(Pos2D pos, char currentChar)
    {
        var relativePos = pos.Subtract(_upperLeftCorner);
        char roomChar = _room.GetCharacterAtPosition(relativePos);
        return MergeChars(currentChar, roomChar);
    }

    private static char MergeChars(char oldChar, char newChar) => oldChar == '+' || newChar == ' ' ? oldChar : newChar;
}
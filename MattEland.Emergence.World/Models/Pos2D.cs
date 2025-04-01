namespace MattEland.Emergence.World.Models;

public record Pos2D(int X, int Y)
{
    public Pos2D Offset(int x, int y) => new(X + x, Y + y);
    public Pos2D Offset(Direction direction) 
        => direction switch
        {
            Direction.Up => Offset(0, -1),
            Direction.Right => Offset(1, 0),
            Direction.Down => Offset(0, 1),
            Direction.Left => Offset(-1, 0),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), actualValue: direction, message: null)
        };
    
}
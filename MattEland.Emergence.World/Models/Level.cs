namespace MattEland.Emergence.World.Models;

public class Level(int width, int height)
{
    public int Width { get; } = width;
    public int Height { get; } = height;

    public FloorType GetFloorInfo(WorldPos pos)
    {
        if (pos.X < 0 || pos.X >= Width || pos.Y < 0 || pos.Y >= Height) return FloorType.Void;
        
        return FloorType.Normal;
    }

    private readonly List<GameObject> _objects = new();
    public IEnumerable<GameObject> Objects => _objects;

    public void AddObject(GameObject obj)
    {
        _objects.Add(obj);
    }
}
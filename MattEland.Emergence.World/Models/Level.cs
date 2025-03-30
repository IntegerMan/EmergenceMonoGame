namespace MattEland.Emergence.World.Models;

public class Level(int width, int height)
{
    private readonly List<GameObject> _objects = [];
    public IEnumerable<GameObject> Objects => _objects;

    public FloorType GetFloorInfo(WorldPos pos)
    {
        if (pos.X < 0 || pos.X >= width || pos.Y < 0 || pos.Y >= height) return FloorType.Void;

        return FloorType.Normal;
    }

    public void AddObject(GameObject obj)
    {
        _objects.Add(obj);
    }
}
namespace MattEland.Emergence.World.Models;

public class Level
{
    private readonly List<GameObject> _objects = new List<GameObject>();
    public IEnumerable<GameObject> Objects => _objects;

    public FloorType GetFloorInfo(WorldPos pos)
    {
        if (pos.X < 0 || pos.X > 10 || pos.Y < 0 || pos.Y > 10) return FloorType.Void;
        
        return FloorType.Normal;
    }

    public void AddObject(GameObject obj)
    {
        _objects.Add(obj);
    }
}
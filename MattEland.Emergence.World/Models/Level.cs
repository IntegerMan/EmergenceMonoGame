using MattEland.Emergence.World.Entities;

namespace MattEland.Emergence.World.Models;

public class Level(int width, int height)
{
    private readonly List<GameObject> _objects = [];
    public IEnumerable<GameObject> Objects => _objects;

    public FloorType GetFloorInfo(Pos2D pos)
    {
        if (pos.X < 0 || pos.X >= width || pos.Y < 0 || pos.Y >= height) return FloorType.Void;
        return FloorType.Normal;
    }

    public void AddObject(GameObject obj)
    {
        _objects.Add(obj);
    }

    public static Level FromData(string data)
    {
        string[] lines = data.Split(['\n', '\r'], StringSplitOptions.RemoveEmptyEntries);

        return FromData(lines);
    }

    private static Level FromData(string[] lines)
    {
        if (lines.Length == 0)
        {
            throw new ArgumentException("Data cannot be empty", nameof(lines));
        }

        int height = lines.Length;
        int width = lines[0].Length;

        Level level = new(width, height);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int x1 = x;
                int y1 = y;
                char c = lines[y1][x1];
                GameObject? obj = BuildGameObjectFromCell(c, () => new Pos2D(x1, y1));
                if (obj is not null)
                {
                    level.AddObject(obj);
                }
            }
        }

        return level;
    }

    private static GameObject? BuildGameObjectFromCell(char c, Func<Pos2D> posFetcher)
    {
        GameObject? obj = null;
        switch (c)
        {
            case '#':
                obj = new Wall(posFetcher());
                break;
            case '.':
            case '_': // Placeholder for a special tile (like a door or something)
            default:
                break;
        }

        return obj;
    }
}
using MattEland.Emergence.World.Entities;
using MattEland.Emergence.World.Models;

namespace MattEland.Emergence.World.Services;

public interface ILevelGenerator
{
    Level Generate(Player player);
}
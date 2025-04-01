using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.ECS.Components;
using MattEland.Emergence.World.Entities;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Entity = DefaultEcs.Entity;

namespace MattEland.Emergence.DesktopClient.ECS.Systems;

public class LevelManagementSystem : ISystem<float>
{
    private readonly DefaultEcs.World _world;

    public LevelManagementSystem(DefaultEcs.World world)
    {
        _world = world;

        LoadLevel();
    }

    private void LoadLevel()
    {
        IWorldService worldService = _world.Get<IWorldService>();
        Level? level = worldService.Level;
        
        if (level is null) throw new InvalidOperationException("Level not loaded");
        
        foreach (var obj in level.Objects)
        {
            Entity entity = _world.CreateEntity();
            entity.Set(new GameObjectComponent(obj));
            if (obj is Player)
            {
                entity.Set<PlayerMovementComponent>();
            }
        }
    }

    public void Update(float state)
    {
    }

    public bool IsEnabled { get; set; } = true;
    
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

}
using System;
using DefaultEcs.System;
using MattEland.Emergence.DesktopClient.ECS.Components;
using MattEland.Emergence.World.Models;
using MattEland.Emergence.World.Services;
using Microsoft.Xna.Framework;
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
        Player player = worldService.Player;
        
        Entity playerEntity = _world.CreateEntity();
        playerEntity.Set(new WorldPositionComponent(player.Pos));
        playerEntity.Set(new RectangleRendererComponent(Color.Cyan));
        playerEntity.Set<PlayerMovementComponent>();
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
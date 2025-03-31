using MattEland.Emergence.World.Models;
using Microsoft.Xna.Framework;

namespace MattEland.Emergence.DesktopClient.ECS.Components;

public record GameObjectComponent(GameObject GameObject)
{
    public Color RenderColor => Color.Yellow;
}
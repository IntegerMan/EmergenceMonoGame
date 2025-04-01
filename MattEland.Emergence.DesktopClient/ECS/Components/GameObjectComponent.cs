using System;
using MattEland.Emergence.World.Entities;

namespace MattEland.Emergence.DesktopClient.ECS.Components;

public record GameObjectComponent(GameObject GameObject)
{
    public Color RenderColor
    {
        get
        {
            string color = GameObject.ForegroundColor;
            
            // Convert from a string like "#557799" to a Color object
            if (!color.StartsWith('#') || color.Length != 7)
            {
                throw new NotSupportedException($"Cannot convert from {color} to a Color object for {GameObject}");
            }

            // TODO: This parsing is a common operation and would benefit from caching or a more efficient implementation
            byte r = byte.Parse(color.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(color.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(color.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            
            return new Color(r, g, b);
        }
    }
}
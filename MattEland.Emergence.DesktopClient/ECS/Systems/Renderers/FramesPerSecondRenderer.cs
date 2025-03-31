using System;
using System.Collections.Generic;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;

/// <summary>
/// A system responsible for rendering the current frames per second (FPS) on the screen.
/// </summary>
public class FramesPerSecondRenderer(DefaultEcs.World world, SpriteBatch spriteBatch) : ISystem<float>
{
    private const int MaxSamples = 100;
    private readonly Queue<float> _fpsSamples = new(MaxSamples);
    private readonly GraphicsManager _graphics = world.Get<GraphicsManager>();

    public void Update(float totalSeconds)
    {
        // Draw the FPS in the top left corner
        float fps = 1f / totalSeconds;
        
        // Add the current FPS to the samples queue
        while (_fpsSamples.Count >= MaxSamples)
        {
            _fpsSamples.Dequeue();
        }
        _fpsSamples.Enqueue(fps);
        
        // Calculate the average FPS
        float averageFps = 0;
        foreach (float sample in _fpsSamples)
        {
            averageFps += sample;
        }
        averageFps /= _fpsSamples.Count;
        
        string fpsText = $"Average FPS: {averageFps:F2}";
        
        // Draw the FPS text using the graphics manager
        spriteBatch.DrawString(_graphics.DebugFont, fpsText, new Vector2(10, 10), Color.White);
    }

    public bool IsEnabled { get; set; } = true;

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
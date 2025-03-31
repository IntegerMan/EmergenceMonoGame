using System;
using System.Collections.Generic;
using DefaultEcs.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;

namespace MattEland.Emergence.DesktopClient.ECS.Systems;

/// <summary>
/// A system responsible for rendering the current frames per second (FPS) on the screen.
/// </summary>
public class FramesPerSecondRenderer : ISystem<float>
{
    private const int MaxSamples = 100;
    private readonly SpriteBatch _spriteBatch;
    private readonly Queue<float> _fpsSamples = new(MaxSamples);
    private readonly GraphicsManager _graphics;

    public FramesPerSecondRenderer(DefaultEcs.World world)
    {
        _graphics = world.Get<GraphicsManager>();
        _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
    }

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
        _spriteBatch.Begin();
        _spriteBatch.DrawString(_graphics.DebugFont, fpsText, new Vector2(10, 10), Color.White);
        _spriteBatch.End();
    }

    public bool IsEnabled { get; set; } = true;

    public void Dispose()
    {
        _spriteBatch.Dispose();
        GC.SuppressFinalize(this);
    }
}
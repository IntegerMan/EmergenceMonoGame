using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ECS.Systems;

namespace MattEland.Emergence.DesktopClient.ECS.Systems;

/// <summary>
/// A system responsible for rendering the current frames per second (FPS) on the screen.
/// </summary>
/// <param name="graphics">The <c>GraphicsManager</c> instance used to access the game's graphics resources.</param>'
public class FramesPerSecondRenderer(GraphicsManager graphics) : IDrawSystem
{
    private const int MaxSamples = 100;
    private readonly SpriteBatch _spriteBatch = new(graphics.GraphicsDevice);
    private readonly Queue<float> _fpsSamples = new(MaxSamples);
    
    public void Initialize(MonoGame.Extended.ECS.World world)
    {
    }

    public void Draw(GameTime gameTime)
    {
        // Draw the FPS in the top left corner
        float fps = 1f / (float)gameTime.ElapsedGameTime.TotalSeconds;
        
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
        _spriteBatch!.Begin();
        _spriteBatch.DrawString(graphics.DebugFont, fpsText, new Vector2(10, 10), Color.White);
        _spriteBatch.End();
    }

    public void Dispose()
    {
        _spriteBatch.Dispose();
        GC.SuppressFinalize(this);
    }
}
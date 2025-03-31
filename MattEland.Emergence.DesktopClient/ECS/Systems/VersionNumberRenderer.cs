using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.ECS.Systems;

namespace MattEland.Emergence.DesktopClient.ECS.Systems;

/// <summary>
/// The <c>VersionDisplaySystem</c> is responsible for displaying the application's version information
/// at a designated position on the screen during the game's rendering phase. It integrates with the
/// Entity Component System (ECS) architecture as a drawing system.
/// </summary>
/// <param name="graphics">The <c>GraphicsManager</c> instance used to access the game's graphics resources.</param>
public class VersionNumberRenderer(GraphicsManager graphics) : IDrawSystem
{
    private readonly SpriteBatch _spriteBatch = new(graphics.GraphicsDevice);
    private string? _text;
    private float _textWidth;
    private Vector2 _position = new(0, 0);

    public void Initialize(MonoGame.Extended.ECS.World world)
    {
        Version version = Assembly.GetEntryAssembly()!.GetName().Version!;
        _text =  $"Emergence v{version} Early Development";
    }

    public void Draw(GameTime gameTime)
    {
        _spriteBatch.Begin();
        
        // Screen size is the same as the window size
        Rectangle screenSize = graphics.Window.ClientBounds;
        Vector2 upperRight = new(screenSize.Width - 10, 10); // Adjust position as needed
        
        // Draw the version string in the top right corner
        if (_textWidth == 0)
        {
            _textWidth = graphics.DebugFont.MeasureString(_text).Width;
            _position = upperRight - new Vector2(_textWidth, 0); // This will need adjustment on resize
        }
        
        _spriteBatch.DrawString(graphics.DebugFont, _text, _position, Color.White);
        _spriteBatch.End();
    }

    public void Dispose()
    {
        _spriteBatch.Dispose();
        GC.SuppressFinalize(this);
    }
}
using System;
using System.Reflection;
using DefaultEcs.System;
using MonoGame.Extended.BitmapFonts;

namespace MattEland.Emergence.DesktopClient.ECS.Systems.Renderers;

/// <summary>
/// The <c>VersionDisplaySystem</c> is responsible for displaying the application's version information
/// at a designated position on the screen during the game's rendering phase. It integrates with the
/// Entity Component System (ECS) architecture as a drawing system.
/// </summary>
public class VersionNumberRenderer : ISystem<float>
{
    private readonly SpriteBatch _spriteBatch;
    private readonly string _text;
    private float _textWidth;
    private Vector2 _position = new(0, 0);
    private readonly BitmapFont _font;
    private readonly GameWindow _window;
    
    public VersionNumberRenderer(DefaultEcs.World world, SpriteBatch spriteBatch)
    {
        _font = world.Get<BitmapFont>();
        _window = world.Get<GameWindow>();
        _spriteBatch = spriteBatch;
        
        Version version = Assembly.GetEntryAssembly()!.GetName().Version!;
        _text =  $"Emergence v{version} Early Development";
    }


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public void Update(float state)
    {
        // Screen size is the same as the window size
        Rectangle screenSize = _window.ClientBounds;
        Vector2 upperRight = new(screenSize.Width - 10, 10); // Adjust position as needed
        
        // Draw the version string in the top right corner
        if (_textWidth == 0)
        {
            _textWidth = _font.MeasureString(_text).Width;
            _position = upperRight - new Vector2(_textWidth, 0); // This will need adjustment on resize
        }
        
        _spriteBatch.DrawString(_font, _text, _position, Color.White);
    }

    public bool IsEnabled { get; set; } = true;
}
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MattEland.Emergence.DesktopClient.Brushes;

public class RectangleBrush : IDisposable
{
    private readonly Texture2D _texture;

    public RectangleBrush(GraphicsDevice graphics)
    {
        _texture = new Texture2D(graphics, 1, 1);
        _texture.SetData([Color.White]);
    }

    public void Dispose()
    {
        _texture.Dispose();
        GC.SuppressFinalize(this);
    }

    public void Render(Rectangle rect, Color color, SpriteBatch spriteBatch)
        => spriteBatch.Draw(_texture, rect, color);
}
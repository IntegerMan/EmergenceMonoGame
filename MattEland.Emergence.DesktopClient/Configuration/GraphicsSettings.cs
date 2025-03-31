using System.Diagnostics.CodeAnalysis;

namespace MattEland.Emergence.DesktopClient.Configuration;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global",
    Justification = "Deserialized values from configuration")]
public class GraphicsSettings
{
    public bool StartFullscreen { get; init; }
    public int TileSize { get; init; } = 32;
    public int TargetFramesPerSecond { get; init; } = 30;
}
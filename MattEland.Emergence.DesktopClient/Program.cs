using System;
using System.Diagnostics.CodeAnalysis;
using MattEland.Emergence.DesktopClient.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;

namespace MattEland.Emergence.DesktopClient;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global",
    Justification = "This is the entry point for the application")]
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            IConfiguration configuration = OptionsConfiguration.BuildConfiguration(args);
            using ServiceProvider provider = DependencyInjectionConfiguration.BuildServiceProvider(configuration);

            using EmergenceGame game = provider.GetRequiredService<EmergenceGame>();
            game.Run();
        }
        catch (Exception ex)
        {
            AnsiConsole.WriteException(ex, ExceptionFormats.ShortenEverything);
        }
    }
}
using MattEland.Emergence.World.Entities;

namespace MattEland.Emergence.Tests.Services;

[TestSubject(typeof(TestLevelGenerator))]
public class LevelServiceTests
{
    [Fact]
    public void CreateLevel_ShouldInitializeLevelWithPlayer()
    {
        // Arrange
        TestLevelGenerator generator = new();
        Player player = new(new Pos2D(0, 0));

        // Act
        Level level = generator.Generate(player);

        // Assert
        level.ShouldNotBeNull();
        level.Objects.ShouldContain(player);
        player.Pos.ShouldBe(new Pos2D(2, 3));
    }
}
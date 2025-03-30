namespace MattEland.Emergence.Tests.Services;

[TestSubject(typeof(TestLevelGenerator))]
public class LevelServiceTests
{
    [Fact]
    public void CreateLevel_ShouldInitializeLevelWithPlayer()
    {
        // Arrange
        TestLevelGenerator generator = new();
        Player player = new(new WorldPos(0, 0));

        // Act
        Level level = generator.Generate(player);

        // Assert
        level.ShouldNotBeNull();
        level.Objects.ShouldContain(player);
        player.Pos.ShouldBe(new WorldPos(2, 3));
    }
}
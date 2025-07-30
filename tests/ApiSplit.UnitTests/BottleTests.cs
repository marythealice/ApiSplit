namespace ApiSplit.UnitTests;

public class BottleTests
{
    
    [Fact]
    public void AddsSplitToBottle()
    {
        // Arrange
        
        var bottle = new Bottle(100m, 17.5m, 1);
        var split = new Split(1, 5m, 1, 17.50m, SplitType.Five);
        
        // Act
        
        bottle.AddSplit(split);
        
        
        // Assert
        
        Assert.NotNull(bottle.Splits);
    }

    [Fact]
    public void CurrentVolumeChangesAfterAddingSplitToBottle()
    {
        
        // Arrange
        var bottle = new Bottle(100m, 17.5m, 1);
        decimal initialVolume = bottle.InitialVolume;
        
        // Act
        
        Assert.Equal(bottle.CurrentVolume, initialVolume);
        var split = new Split(1, 5m, 1, 17.50m, SplitType.Five);
        bottle.AddSplit(split);
        
        // Assert
        
        Assert.True(bottle.CurrentVolume == 95m);
    }
    
    [Fact]
    public void Given_SplitVolumeGreaterThanBottleVolume_When_AddingSplit_Then_SplitIsNotAdded()
    {
        // Arrange
        var bottle = new Bottle(10m, 17.5m, 1);
        var split = new Split(1, 15m, 1, 17.50m, SplitType.Fifteen);
        
        // Act
        var wasSplitAdded = bottle.AddSplit(split);
        
        // Assert
        Assert.False(wasSplitAdded);
    }
    
}

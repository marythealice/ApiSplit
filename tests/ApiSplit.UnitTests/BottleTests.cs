using ApiSplit.Models;

namespace ApiSplit.UnitTests;

public class BottleTests
{
    
    [Fact]
    public void Given_Enough_Volume_In_Bottle_Add_Split()
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

    public void Given_Adding_Split_To_Bottle_Current_Volume_Changes()
    {
        
        // Arrange
        var bottle = new Bottle(100m, 17.5m, 1);
        decimal initialVolume = bottle.InitialVolume;
        
        // Act
        
        Assert.Equal(bottle.CurrentVolume, initialVolume);
        var split = new Split(1, 5m, 1, 17.50m, SplitType.Five);
        bottle.AddSplit(split);
        
        // Assert
        
        Assert.Equal(95m, bottle.CurrentVolume);
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

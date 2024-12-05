using Chirp.Core.DTOs;
namespace Chirp.Tests;

public class CheepDtoUnitTests
{
    
    [Fact]
    public void TestConvertDateTimeToString()
    {
        // Arrange
        var currentTime = DateTime.Now;
        var cheepDto = new CheepDto(2,"author", "message", currentTime);
        
        // Act
        var result = cheepDto.Timestamp;
        
        // Assert
        Assert.Equal(currentTime.ToString("yyyy-MM-dd HH:mm:ss"), result);
    }
}

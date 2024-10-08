namespace Chirp.Razor.Tests;

public class CheepDtoUnitTests
{
    
    [Fact]
    public void TestConvertDateTimeToString()
    {
        var cheepDto = new CheepDto("author", "message", new DateTime(2021, 1, 1, 12, 0, 0));
        var result = cheepDto.Timestamp;
        // Assert
        Assert.Equal("2021-01-01 12:00:00", result);
    }
}
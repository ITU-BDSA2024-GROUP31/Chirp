using System.IO;               
using System.Linq;             
using Xunit;                   
using System.Collections.Generic; 
using CsvHelper;               
using CsvHelper.Configuration;
using System.Globalization;
using SimpleDB;

public class CsvDatabaseTests
{
    private const string TestFilePath = "chirp_cli_db.csv";

    // Ensure test file is empty before each test
    public CsvDatabaseTests()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }

    // Test 1: Singleton Pattern
    [Fact]
    public void SingletonPattern_Test()
    {
        // Arrange & Act
        var instance1 = CsvDatabase<Cheep>.Instance;
        var instance2 = CsvDatabase<Cheep>.Instance;

        // Assert
        Assert.Same(instance1, instance2);
    }

    // Test 2: Read Method Returns Correct Number of Records
    [Fact]
    public void ReadMethod_ReturnsCorrectNumberOfRecords()
    {
        // Arrange
        var csvDatabase = CsvDatabase<Cheep>.Instance;
        var csvContent = "Author,Message,Timestamp\nuser1,message1,1620000000\nuser2,message2,1620000001";
        File.WriteAllText(TestFilePath, csvContent);

        // Act
        var records = csvDatabase.Read(2); // Read with limit

        // Assert
        Assert.Equal(2, records.Count()); // Check that the number of records matches the limit.
    }
    
    // Test 3: Check if the time conversion is working properly with an ai generated actual result as test
    [Fact]
    public void ToUnixTimeSeconds_ShouldReturnCorrectUnixTimestamp()
    {
        // Arrange
        var dateTimeOffset = new DateTimeOffset(2023, 9, 25, 12, 0, 0, TimeSpan.Zero);
        // Expected Unix timestamp for 2023-09-25 12:00:00 UTC
        var expectedUnixTime = 1695643200;  

        // Act
        var actualUnixTime = dateTimeOffset.ToUnixTimeSeconds();

        // Assert
        Assert.Equal(expectedUnixTime, actualUnixTime);
    }


}
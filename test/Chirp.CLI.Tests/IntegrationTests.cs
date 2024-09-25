using System.IO;               
using System.Linq;             
using Xunit;                   
using System.Collections.Generic; 
using CsvHelper;               
using CsvHelper.Configuration;
using System.Globalization;
using SimpleDB;  
public class IntegrationTests
{
    private const string TestFilePath = "chirp_cli_db.csv";

    // Ensure test file is empty before each test
    public IntegrationTests()
    {
        if (File.Exists(TestFilePath))
        {
            File.Delete(TestFilePath);
        }
    }
    
    // Test 1: Store Method Writes Record to File
    [Fact]
    public void StoreMethod_WritesRecordToFile()
    {
        // Arrange
        var csvDatabase = CsvDatabase<Cheep>.Instance;
        var newCheep = new Cheep("test_user", "test_message", 1620000000);

        // Act
        csvDatabase.Store(newCheep);

        // Read the file to verify the entry was added
        var allText = File.ReadAllText(TestFilePath);

        // Assert
        Assert.Contains("test_user", allText);     // Ensure the file contains the author
        Assert.Contains("test_message", allText);  // Ensure the file contains the message
        Assert.Contains(1620000000.ToString(), allText);    // Ensure the file contains the timestamp
    }
}

    
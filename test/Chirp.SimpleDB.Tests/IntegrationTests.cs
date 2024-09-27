using SimpleDB;

namespace Chirp.SimpleDB.Tests;

public class UnitTest1
{
    [Fact]
    public void TestStoringCheeps()
    {
        // Arrange
        var cheepDb = CsvDatabase<Cheep>.Instance;

        var cheeps = new List<Cheep>
        {
            new Cheep("Alice", "Enjoying the sunny weather!", 1632768000),
            new Cheep("Bob", "Just finished a 5k run!", 1632854400),
            new Cheep("Charlie", "Learning Go is fun!", 1632940800),
            new Cheep("Dana", "Building cool projects in .NET", 1633027200),
            new Cheep("Eve", "Network configurations are tricky!", 1633113600)
        };

        // Act
        foreach (var chp in cheeps)
        {
            cheepDb.Store(chp);

        }

        // Assert
        Assert.Equal(cheeps.Count + cheepDb.Read(10).Count(), cheepDb.Read(15).Count());
    }
    
    
    [Fact]
    public void TestReadingCheeps()
    {
        // Arrange
        var cheepDb = CsvDatabase<Cheep>.Instance;
        
        var cheeps = new List<Cheep> {
            new Cheep("Alice", "Enjoying the sunny weather!", 1632768000),
            new Cheep("Eve", "Network configurations are tricky!", 1633113600)
        };
        
        // Act
        foreach (var chp in cheeps) 
        {
            cheepDb.Store(chp);

        }
        
        // Assert
        Assert.Contains(cheeps[0], cheepDb.Read(15));
        Assert.Contains(cheeps[1], cheepDb.Read(15));


    }
}
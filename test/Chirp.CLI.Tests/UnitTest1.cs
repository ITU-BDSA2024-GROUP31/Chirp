using SimpleDB;

namespace Chirp.CLI.Tests;

public class UnitTest1
{
    //Can be deleted when making unitTests, made just to make sure that we could get the access to csv
    [Fact]
    public void Test1()
    {
        // made just to make sure that we could get the access to csv
        var cheepDb = CsvDatabase<Cheep>.Instance;
        Console.WriteLine(cheepDb.Read(1));
        
    }
}
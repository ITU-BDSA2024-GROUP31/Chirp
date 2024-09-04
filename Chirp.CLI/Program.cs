using SimpleDB;
using DocoptNet;

CsvDatabase<Cheep> cheepDb = new CsvDatabase<Cheep>();


const string usage = @"Chirp CLI version.

Usage:
  chirp read <limit>
  chirp cheep <message> 
  chirp (-h | --help)
  chirp --version

Options:
  -h --help     Show this screen.
  --version     Show version.
";

var arguments = new Docopt().Apply(usage, args, version: "0.1", exit: true)!;

//Check weather the first command line is read
if (arguments["read"].IsTrue)
{
    try
    {
        var limit = arguments["<limit>"].AsInt; 
        var cheeps = cheepDb.Read(limit);
        foreach (var cheep in cheeps)
        {
            var formattedTime = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp).ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"{cheep.Author} @ {formattedTime}: {cheep.Message}");
        }
    } catch (IOException e) 
    { 
        Console.WriteLine("The file could not be read:"); 
        Console.WriteLine(e.Message); 
    } 
}
else if (arguments["cheep"].IsTrue) // Checks if "cheep" is the first in the command line
{
    try
    {
        var message = arguments["<message>"].ToString();
        cheepDb.Store(new Cheep(Environment.UserName, message, DateTimeOffset.Now.ToUnixTimeSeconds()));
    }
    catch (IOException e)
    {
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
    }
}
else
{
    Console.WriteLine("Invalid command");
}

public record Cheep(string Author, string Message, long Timestamp);
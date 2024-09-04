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

    //check if "read" appears
// run read by typing: dotnet run -- read <limit>
if(arguments["read"].IsTrue)

{
    try
    {
        var cheeps = cheepDb.Read();
        foreach (var cheep in cheeps)
        {
            Console.WriteLine($"{cheep.Author} cheeped: {cheep.Message} at {DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp)}");
        }
    } catch (IOException e) 
    { 
        Console.WriteLine("The file could not be read:"); 
        Console.WriteLine(e.Message); 
    } 
} else if(arguments["cheep"].IsTrue) //check if cheep appears
{
     try {
            cheepDb.Store(new Cheep(Environment.UserName, args[1], DateTimeOffset.Now.ToUnixTimeSeconds()));
        } catch (IOException e) {
            Console.WriteLine("The file could not be written:");
            Console.WriteLine(e.Message);
        } catch (IndexOutOfRangeException) {
            Console.WriteLine("Please provide a message to cheep");
        } 
}
else
{
    Console.WriteLine("Invalid command");
}

public record Cheep(string Author, string Message, long Timestamp);

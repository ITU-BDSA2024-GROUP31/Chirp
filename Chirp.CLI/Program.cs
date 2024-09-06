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
  
        var limit = arguments["<limit>"].AsInt; 
        UserInterface.Read(cheepDb, limit);
}
else if (arguments["cheep"].IsTrue) // Checks if "cheep" is the first in the command line
{

        var message = arguments["<message>"].ToString();
        UserInterface.Cheep(cheepDb, message);
}
else
{
    Console.WriteLine("Invalid command");
}

public record Cheep(string Author, string Message, long Timestamp);
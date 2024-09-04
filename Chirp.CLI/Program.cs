using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DocoptNet;




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
//Check weather the firs command line is read
if (arguments["read"].IsTrue)
{
    // 
    try
    {
        ReadCheeps();

    }
    catch (IOException e)
    {
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
    }
}
else if (arguments["cheep"].IsTrue) // Checks if cheep is the first in the command line
{
    try
    {
        var message = arguments["message"].ToString();
        WriteCheep(message);
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




 static void ReadCheeps()
{
    using (var reader = new StreamReader("Data/chirp_cli_db.csv"))
    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
           {
               HasHeaderRecord = true,
           }))
    {
        var records = csv.GetRecords<Cheep>().ToList();
        foreach (var record in records)
        {
            var date = DateTimeOffset.FromUnixTimeSeconds(record.Timestamp);
            var formattedDate = date.ToString("dd/MM/yyyy HH:mm:ss");
            Console.WriteLine($"{record.Author} @ {formattedDate}: {record.Message}");
        }
    }
}
    
 static void WriteCheep(string message)
{
    var newCheep = new Cheep
    {
        Author = Environment.UserName,
        Message = message,
        Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
    };

    using (var writer = new StreamWriter("Data/chirp_cli_db.csv", append: true))
    using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
    {
        csv.WriteRecord(newCheep);
        writer.WriteLine();
    }
}
 public record Cheep
 {
     public required string Author { get; set; }
     public required string Message { get; set; }
     public long Timestamp { get; set; }
 };
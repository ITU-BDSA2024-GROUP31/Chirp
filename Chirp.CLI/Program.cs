using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using System.IO;
using System.Linq;

public class Program
{
    
    public record Cheep
    {
        public required string Author { get; set; }
        public required string Message { get; set; }
        public long Timestamp { get; set; }
    }
    

    public static void Main(string[] args)
    {
        if (args[0] == "read")
        {
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
        else if (args[0] == "cheep")
        {
            try
            {
                WriteCheep(args[1]);
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be written:");
                Console.WriteLine(e.Message);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Please provide a message to cheep");
            }
        }
        else
        {
            Console.WriteLine("Invalid command");
        }
    }

    private static void ReadCheeps()
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

     private static void WriteCheep(string message)
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
}
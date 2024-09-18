
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/cheeps", () => Read());
app.MapPost("/cheep", Store);

app.Run();

IEnumerable<Cheep> Read(int? limit = null)
{
    using (var reader = new StreamReader("../../data/chirp_cli_db.csv"))
    using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
           {
               HasHeaderRecord = true,
           }))
    {
        var records = limit.HasValue ? csv.GetRecords<Cheep>().Take(limit.Value).ToList() : csv.GetRecords<Cheep>().ToList();
        return records;
    }
}

void Store(Cheep record)
{
    using (var writer = new StreamWriter("../../data/chirp_cli_db.csv", append: true))
    using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
    {
        csv.WriteRecord(record);
        writer.WriteLine();
    }
}

public record Cheep(string Author, string Message, long Timestamp);
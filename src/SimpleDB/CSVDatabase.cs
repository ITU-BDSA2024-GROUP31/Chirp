namespace SimpleDB;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public sealed class CsvDatabase<T> : IDatabaseRepository<T>
{
    public IEnumerable<T> Read(int? limit = null)
    {
        using (var reader = new StreamReader("Data/chirp_cli_db.csv"))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   HasHeaderRecord = true,
               }))
        {
            var records = limit.HasValue ? csv.GetRecords<T>().Take(limit.Value).ToList() : csv.GetRecords<T>().ToList();
            return records;
        }
    }

    public void Store(T record)
    {
        using (var writer = new StreamWriter("Data/chirp_cli_db.csv", append: true))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecord(record);
            writer.WriteLine();
        }
    }
}
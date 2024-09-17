namespace SimpleDB;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

public sealed class CsvDatabase<T> : IDatabaseRepository<T>
{
    // Private static instance for Singleton pattern
    private static readonly Lazy<CsvDatabase<T>> _instance = new Lazy<CsvDatabase<T>>(() => new CsvDatabase<T>());

    private readonly string _path;

    // Private constructor to prevent external instantiation
    private CsvDatabase()
    {
        _path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "chirp_cli_db.csv");
    }
    public static CsvDatabase<T> Instance => _instance.Value;
    public IEnumerable<T> Read(int? limit = null)
    {
        using (var reader = new StreamReader(_path))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   HasHeaderRecord = true,
               }))
        {
            var records = csv.GetRecords<T>().ToList();
            return limit.HasValue ? records.TakeLast(limit.Value).ToList() : records;
        }
    }

    public void Store(T record)
    {
        using (var writer = new StreamWriter(_path, append: true))
        using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csv.WriteRecord(record);
            writer.WriteLine();
        }
    }
    
    
}
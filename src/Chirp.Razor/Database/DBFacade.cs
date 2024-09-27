using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.FileProviders;


namespace Chirp.Razor;

public class DBFacade
{
    private readonly SqliteConnection _connection;

    public DBFacade()
    {
        var path = Environment.GetEnvironmentVariable("CHIRPDBPATH");
        if (path != null)
        {
            _connection = new SqliteConnection($"Data Source={path}");
            _connection.Open();
        }
        else
        {
            path = Path.GetTempPath() + "/chirp.db";
            _connection = new SqliteConnection($"Data Source={path}");
            _connection.Open();

            SetupTmpDB(_connection);
        }
    }

    private void SetupTmpDB(SqliteConnection connection)
    {
        var embeddedProvider = new EmbeddedFileProvider(Assembly.GetExecutingAssembly());
        using (var reader = embeddedProvider.GetFileInfo("schema.sql").CreateReadStream())
        using (var sr = new StreamReader(reader))
        {
            var query = sr.ReadToEnd();
            var command = _connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteReader();
        }

        using (var reader = embeddedProvider.GetFileInfo("dump.sql").CreateReadStream())
        using (var sr = new StreamReader(reader))
        {
            var query = sr.ReadToEnd();
            var command = _connection.CreateCommand();
            command.CommandText = query;
            command.ExecuteReader();
        }
    }


    public List<CheepViewModel> GetCheeps()
    {
        // List contating our query results
        var cheeps = new List<CheepViewModel>();


        try
        {
            var sqlQuery =
                "SELECT m.text, m.pub_date, u.username FROM message m JOIN user u ON m.author_id = u.user_id ORDER BY m.pub_date desc";


            var command = _connection.CreateCommand();
            command.CommandText = sqlQuery;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var message = reader.GetString(0);
                var pubDate = reader.GetInt64(1);
                var author = reader.GetString(2);

                cheeps.Add(new CheepViewModel(message, author, DateTimeUtils.UnixTimeStampToDateTimeString(pubDate)));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return cheeps;
    }


    public List<CheepViewModel> GetCheepsFromAuthor(string authorParameter)
    {
        var cheeps = new List<CheepViewModel>();

        try
        {
            var sqlQuery =
                "SELECT m.text, m.pub_date, u.username FROM message m JOIN user u ON m.author_id = u.user_id WHERE u.username = @author ORDER BY m.pub_date desc";

            var command = _connection.CreateCommand();
            command.CommandText = sqlQuery;
            command.Parameters.Add("@author", SqliteType.Text);
            command.Parameters["@author"].Value = authorParameter;

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var message = reader.GetString(0);
                var pubDate = reader.GetInt64(1);
                var author = reader.GetString(2);

                cheeps.Add(
                    new CheepViewModel(message, author, DateTimeUtils.UnixTimeStampToDateTimeString(pubDate)));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return cheeps;
    }
}

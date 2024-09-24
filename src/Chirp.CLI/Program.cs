using System.Net.Http.Headers;
using System.Net.Http.Json;
using SimpleDB;
using DocoptNet;

CsvDatabase<Cheep> cheepDb = CsvDatabase<Cheep>.Instance;

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

var arguments = new Docopt().Apply(usage, args, version: "0.0.4", exit: true)!;

var baseURL = "https://bdsagroup31chirpremotedb.azurewebsites.net/";
using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.BaseAddress = new Uri(baseURL);

if (arguments["read"].IsTrue)
{
    try
    {
        var limit = arguments["<limit>"].AsInt;
        var cheeps = await client.GetFromJsonAsync<List<Cheep>>($"cheeps?limit={limit}");
        if (cheeps is null)
        {
            Console.WriteLine("No cheeps found");
            return;
        }
        UserInterface.PrintCheeps(cheeps);
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Request error:");
        Console.WriteLine(e.Message);
    }
}
else if (arguments["cheep"].IsTrue)
{
    try
    {
        var message = arguments["<message>"].ToString();
        var username = Environment.UserName;
        var cheep = new Cheep(username, message, DateTimeOffset.Now.ToUnixTimeSeconds());
        await client.PostAsJsonAsync("cheep", cheep);
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine("Request error:");
        Console.WriteLine(e.Message);
    }
}
else
{
    Console.WriteLine("Invalid command");
}

public record Cheep(string Author, string Message, long Timestamp);
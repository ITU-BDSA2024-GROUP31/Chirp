
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
List<Cheep> list = new List<Cheep>();

app.MapGet("/cheeps", (int? limit) =>
{
    var cheeps = limit.HasValue ? list.Take(limit.Value).ToList() : list;
    return Results.Ok(cheeps);
});
app.MapPost("/cheep", (Cheep cheep) => list.Add(cheep));

app.Run();

public record Cheep(string Author, string Message, long Timestamp);
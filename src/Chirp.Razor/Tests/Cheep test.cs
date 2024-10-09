using Chirp.Razor.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Chirp.Razor.Tests;

public class Cheep_test : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ChatDbContext _context;
    private readonly ICheepRepository _repository;

    public Cheep_test() 
    {
        _connection = new SqliteConnection("Filename=:memory:"); 
        _connection.OpenAsync().Wait(); 

        var builder = new DbContextOptionsBuilder<ChatDbContext>()
            .UseSqlite(_connection);

        _context = new ChatDbContext(builder.Options);
        _context.Database.EnsureCreatedAsync().Wait(); 

        _repository = new CheepRepository(_context);
        DbInitializer.SeedDatabase(_context);
    }

    [Fact]
    public void TestCheepInfo()
    {
        
        var allCheeps =  _repository.ReadAllCheeps();

        
        Assert.NotNull(allCheeps);
        Assert.NotNull(_context.Authors);

        
        
    }

    [Fact]
    public void TestNumOfCheepsForAuthor()
    {
        

        foreach (var author in _context.Authors)
        {
            if (author.Name == "Helge")
            {
                Assert.True(author.Cheeps.Count == 1);
            }
        }
        
        Dispose();

    }

    [Fact]
    public void testContentOfCheep()
    {
        foreach (var C in _context.Cheeps)
        {
            if (C.AuthorId == 11) 
            {
                Assert.True(C.Text == "Hello, BDSA students!");
            }
        }
        
        Dispose();
        
    }


    public void Dispose() // Cleanup after each test
    {
        _context.Dispose(); // Dispose of the DbContext
        _connection.Dispose(); // Dispose of the SQLite connection
    }
}
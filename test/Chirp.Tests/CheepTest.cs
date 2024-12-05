using Chirp.Infrastructure.Repositories;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.Database;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Tests;

public class CheepTest : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly ChatDbContext _context;
    private readonly ICheepRepository _cheepRepository;

    public CheepTest() 
    {
        _connection = new SqliteConnection("Filename=:memory:"); 
        _connection.OpenAsync().Wait(); 

        var builder = new DbContextOptionsBuilder<ChatDbContext>()
            .UseSqlite(_connection);

        _context = new ChatDbContext(builder.Options);
        _context.Database.EnsureCreatedAsync().Wait(); 

        _cheepRepository = new CheepRepository(_context);
        DbInitializer.SeedTestDatabase(_context);
    }

    [Fact]
    public void TestCheepInfo()
    {
        // Act
        var allCheeps =  _cheepRepository.ReadAllCheeps();

        // Assert
        Assert.NotNull(allCheeps);
        Assert.NotNull(_context.Authors);
    }

    [Fact]
    public void TestNumOfCheepsForAuthor()
    {
        
        // Act
        var author = _context.Authors.Include(author => author.Cheeps).FirstOrDefault(a => a.Name == "Helge");

        // Assert
        Assert.NotNull(author);
        Assert.True(author.Cheeps.Count == 1);

        // foreach (var author in _context.Authors)
        // {
        //     if (author.Name == "Helge")
        //     {
        //         Assert.True(author.Cheeps.Count == 1);
        //     }
        // }
        
        Dispose();

    }

    [Fact]
    public void TestContentOfCheep()
    {
        // Act
        var cheep = _context.Cheeps.FirstOrDefault(c => c.AuthorId == 11);

        // Assert
        Assert.NotNull(cheep);
        Assert.Equal("Hello, BDSA students!", cheep.Text);

        // foreach (var C in _context.Cheeps)
        // {
        //     if (C.AuthorId == 11) 
        //     {
        //         Assert.True(C.Text == "Hello, BDSA students!");
        //     }
        // }

        Dispose();
        
    }
    
    [Fact]
    public void TestIfCheepIsDeletedFromDb ()
    {
        // Arrange
        var cheep = _context.Cheeps.FirstOrDefault(c => c.CheepId == 1);
        
        // Act
        _cheepRepository.DeleteCheep(cheep?.CheepId ?? 0);
        
        // Assert
        var noCheepCheck = _context.Cheeps.FirstOrDefault(c => c.CheepId == 1);
        Assert.Null(noCheepCheck);
        
        Dispose();
    }


    public void Dispose() // Cleanup after each test
    {
        _context.Dispose(); // Dispose of the DbContext
        _connection.Dispose(); // Dispose of the SQLite connection
    }
}
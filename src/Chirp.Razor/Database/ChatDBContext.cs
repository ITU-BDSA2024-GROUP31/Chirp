using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor;

public class ChatDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }
    
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
        DbInitializer.SeedDatabase(this);
    }
    
    
}
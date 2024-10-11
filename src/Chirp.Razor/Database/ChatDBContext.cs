using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor;

public class ChatDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }
    
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // TOODO: dotnet ef migrations add UniqueAuthorNamesAndEmails
        //implement it they all are on one modelbuilder
        base.OnModelCreating(modelBuilder);
    
        modelBuilder.Entity<Author>()
            .HasIndex(c => c.Name)
            .IsUnique();
        modelBuilder.Entity<Author>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}
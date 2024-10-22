using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
        // Ensures that author names and email are required and unique
        base.OnModelCreating(modelBuilder);

        /** modelBuilder.Entity<Author>()
            .HasIndex(c => c.Name)
            .IsUnique();
            **/
        
        /**modelBuilder.Entity<Author>()
            .HasIndex(c => c.Email)
            .IsUnique();
        
        modelBuilder.Entity<Author>()
            .Property(c => c.Name)
            .IsRequired();
        
        modelBuilder.Entity<Author>()
            .Property(c => c.Email)
            .IsRequired(); 
        **/
            
    }
}
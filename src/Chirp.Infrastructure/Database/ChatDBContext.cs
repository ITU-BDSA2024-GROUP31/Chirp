using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Chirp.Core.Entities;

namespace Chirp.Infrastructure.Database;

public class ChatDbContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }
    
    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }
}
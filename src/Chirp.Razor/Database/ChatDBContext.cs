using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Chirp.Razor;

public class ChatDbContext : IdentityDbContext<Author, IdentityRole<int>, int>
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Cheep> Cheeps { get; set; }
    public DbSet<Follower> Followers { get; set; }

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Follower>()
            .HasKey(f => new { f.FollowerId, f.FolloweeId });

        builder.Entity<Follower>()
            .HasOne(f => f.FollowerAuthor)
            .WithMany(a => a.Following)
            .HasForeignKey(f => f.FollowerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Follower>()
            .HasOne(f => f.FolloweeAuthor)
            .WithMany(a => a.Followers)
            .HasForeignKey(f => f.FolloweeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
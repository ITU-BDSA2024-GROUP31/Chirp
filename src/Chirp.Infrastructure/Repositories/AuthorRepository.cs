using Chirp.Core.Entities;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Chirp.Razor.Repositories
{
        public class AuthorRepository : IAuthorRepository
        {
                private readonly ChatDbContext _context;

                public AuthorRepository(ChatDbContext context)
                {
                        _context = context;
                }

                public async Task<Author?> FindAuthorByName(string userName)
                {
                        return await _context.Authors.FirstOrDefaultAsync(a => a.UserName == userName);
                }

                public async Task<Author?> FindAuthorByEmail(string email)
                {
                        return await _context.Authors.FirstOrDefaultAsync(a => a.Email == email);
                }
                public async Task<Author?> FindAuthorById(int authorId)
                {
                        return await _context.Authors.FirstOrDefaultAsync(a => a.Id == authorId);
                }


                public async Task<Author> NewAuthor(int id, string name, string email, List<Cheep> cheeps)
                {
                        var nwAuthor = new Author
                        {
                                Id = id,
                                Name = name,
                                Email = email,
                                Cheeps = cheeps
                        };

                        await _context.Authors.AddAsync(nwAuthor);
                        await _context.SaveChangesAsync();

                        return nwAuthor;
                }
                public async Task FollowAuthor(int followerId, int followeeId)
                {
                        // Prevent self-following
                        if (followerId == followeeId) return;

                        // Check if the relationship already exists
                        var existingFollower = await _context.Followers
                            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);

                        if (existingFollower != null)
                        {
                                // If the relationship already exists, no need to add again
                                return;
                        }

                        // Retrieve followee
                        var followee = await _context.Authors
                            .FirstOrDefaultAsync(a => a.Id == followeeId);

                        // Retrieve the follower and include the Following navigation property
                        var follower = await _context.Authors
                            .Include(a => a.Following)  // Ensure we include the Following collection
                            .FirstOrDefaultAsync(a => a.Id == followerId);

                        // If both exist, create the relationship and update the in-memory collection
                        if (followee != null && follower != null)
                        {
                                var followerRecord = new Follower
                                {
                                        FollowerId = followerId,
                                        FolloweeId = followeeId,
                                        FollowerAuthor = follower,
                                        FolloweeAuthor = followee
                                };

                                // Add to the database
                                _context.Followers.Add(followerRecord);
                                Console.WriteLine("Added to the database" + followerRecord);

                                // Add to the in-memory Following collection for the current user
                                follower.Following.Add(followerRecord);  // Ensure the in-memory collection is updated

                                // Save changes to the database
                                await _context.SaveChangesAsync();
                        }
                }




                public async Task UnfollowAuthor(int followerId, int followeeId)
                {
                        var followerRecord = await _context.Followers
                            .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);

                        if (followerRecord != null)
                        {
                                _context.Followers.Remove(followerRecord);
                                await _context.SaveChangesAsync();
                        }
                }
        }
}
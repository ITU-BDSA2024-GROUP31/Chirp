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
            if (followerId == followeeId) return;

            var followee = await _context.Authors
                .Include(a => a.Followers)
                .FirstOrDefaultAsync(a => a.Id == followeeId);

            var follower = await _context.Authors.FindAsync(followerId);

            
                var followerRecord = new Follower
                {
                    FollowerId = followerId,
                    FolloweeId = followeeId,
                    FollowerAuthor = follower,
                    FolloweeAuthor = followee
                };

                _context.Followers.Add(followerRecord);
                await _context.SaveChangesAsync();
            
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
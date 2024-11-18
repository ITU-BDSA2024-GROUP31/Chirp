using Microsoft.EntityFrameworkCore;

namespace Chirp.Razor.Repositories;

public class AuthorRepository : IAuthorRepository
{
        private readonly ChatDbContext _context;

        public AuthorRepository(ChatDbContext context)
        {
                _context = context;
        }

        public async Task<Author?> FindAuthorByName(string userName)
        {
                var author = await _context.Authors.Where(a => a.Name == userName).FirstOrDefaultAsync();

                return author;
        }

        public async Task<Author?> FindAuthorByEmail(string email)
        {
                var author = await _context.Authors.Where(a => a.Email == email).FirstOrDefaultAsync();

                return author;
        }

        public async Task<Author> NewAuthor(int id, string name, string email, List<Cheep> cheeps)
        {
                var nwAuthor = new Author()
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

        public async Task FollowAuthor(int followerId, int beingFollowedId)
        {
                var follower = await _context.Authors.FindAsync(followerId);
                var beingFollowed = await _context.Authors.FindAsync(beingFollowedId);

                if (follower != null && beingFollowed != null)
                {
                        beingFollowed.Followers.Add(follower);
                        await _context.SaveChangesAsync();
                }
        }
}
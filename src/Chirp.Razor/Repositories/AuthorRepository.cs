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
                        AuthorId = id,
                        Name = name,
                        Email = email,
                        Cheeps = cheeps
                        
                };

                await _context.Authors.AddAsync(nwAuthor);
                await _context.SaveChangesAsync();

                return nwAuthor;
        }
}
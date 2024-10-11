using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Chirp.Razor.Repositories
{
    public class CheepRepository : ICheepRepository
    {
        private readonly ChatDbContext _context;

        public CheepRepository(ChatDbContext context)
        {
            _context = context;
        }

        public async Task<List<CheepDto>> ReadCheepsFromAuthor(string userName)
        {
            var cheeps = await _context.Cheeps
                .Include(c => c.Author)
                .Where(c => c.Author.Name == userName)
                .OrderByDescending(c => c.Timestamp)
                .Select(c => new CheepDto(c.Author.Name, c.Text, c.Timestamp))
                .ToListAsync();

            return cheeps;
        }

        public async Task<List<CheepDto>> ReadAllCheeps()
        {
            var cheeps = await _context.Cheeps
                .Include(c => c.Author)
                .OrderByDescending(c => c.Timestamp)
                .Select(c => new CheepDto(c.Author.Name, c.Text, c.Timestamp))
                .ToListAsync();

            return cheeps;
        }

        public async Task<List<CheepDto>> NewCheep(string text, string userName)
        {
            var author = await _context.Authors.Where(a => a.Name == userName).FirstOrDefaultAsync();

            if (author == null)
            {
                var nwAuthor = new Author()
                {
                    Name = userName
                };

                await _context.Authors.AddAsync(nwAuthor);
                await _context.SaveChangesAsync(); 
            }
            
            
            var nwCheep = new Cheep()
            {
                Text = text,
                AuthorId = author.AuthorId,
                Author = author,
                Timestamp = DateTime.Now
            };

            var nwListCheep = new List<CheepDto>()
            {
                new(nwCheep.Author.Name, nwCheep.Text, nwCheep.Timestamp)
            };
            
            await _context.Cheeps.AddAsync(nwCheep);
            await _context.SaveChangesAsync();

            return nwListCheep;
        }
    }
}
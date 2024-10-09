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

        public Task<List<CheepDto>> CreateNewCheep(string text, string userName)
        {
            throw new NotImplementedException();
        }
    }
}
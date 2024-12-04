using Microsoft.EntityFrameworkCore;
using Chirp.Core.Entities;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Core.DTOs;
using Chirp.Infrastructure.Database;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Chirp.Infrastructure.Repositories
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

            var author = await _context.Authors
                .Include(a => a.Cheeps)
                .FirstOrDefaultAsync(a => a.Name == userName);

            if (author == null)
            {

                author = new Author
                {
                    Name = userName,
                    Email = null,
                    Cheeps = new List<Cheep>()
                };

                await _context.Authors.AddAsync(author);
                await _context.SaveChangesAsync();
            }


            var newCheep = new Cheep
            {
                Text = text,
                AuthorId = author.Id,
                Author = author,
                Timestamp = DateTime.Now
            };

            author.Cheeps.Add(newCheep);


            await _context.Cheeps.AddAsync(newCheep);


            await _context.SaveChangesAsync();


            var cheepList = author.Cheeps
                .Select(c => new CheepDto(c.Author.Name, c.Text, c.Timestamp))
                .ToList();

            return cheepList;
        }
    }
}
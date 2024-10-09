namespace Chirp.Razor.Repositories;

public class AuthorRepository : IAuthorRepository
{
        private readonly ChatDbContext _context;

        public AuthorRepository(ChatDbContext context)
        {
                _context = context;
        }

        public Task<Author> FindAuthorByName(string userName)
        {
                throw new NotImplementedException();
        }

        public Task<Author> FindAuthorByEmail(string email)
        {
                throw new NotImplementedException();
        }

        public Task<Author> CreateNewAuthor(int authorId, string name, string email)
        {
                throw new NotImplementedException();
        }
}
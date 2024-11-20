using Chirp.Razor.Repositories;

namespace Chirp.Razor
{
    public interface ICheepService
    {
        public List<CheepDto> GetCheeps(int page);
        public List<CheepDto> GetCheepsFromAuthor(string author, int page);
        public List<CheepDto> CreateNewCheep(string text, string userName);
        public Author? GetAuthorByName(string name);
        public Author? GetAuthorByEmail(string email);
        public Author CreateNewAuthor(int id, string name, string email, List<Cheep> cheeps);
    }

    public class CheepService : ICheepService
    {
        private readonly ICheepRepository _cheepRepository;
        private readonly IAuthorRepository _authorRepository;

        private const int PageSize = 32;

        public CheepService(ICheepRepository cheepRepository, IAuthorRepository authorRepository)
        {
            _cheepRepository = cheepRepository;
            _authorRepository = authorRepository;
        }

        public List<CheepDto> GetCheeps(int page)
        {
            int pagesToSkip = (page - 1) * PageSize;
            return _cheepRepository.ReadAllCheeps().Result.Skip(pagesToSkip).Take(PageSize).ToList();
        }

        public List<CheepDto> GetCheepsFromAuthor(string author, int page)
        {
            int pagesToSkip = (page - 1) * PageSize;
            return _cheepRepository.ReadCheepsFromAuthor(author).Result.Skip(pagesToSkip).Take(PageSize).ToList();
        }
        
        public List<CheepDto> CreateNewCheep(string text, string userName)
        {
            return _cheepRepository.NewCheep(text, userName).Result;
        }
        
        public Author? GetAuthorByName(string name)
        {
            return _authorRepository.FindAuthorByName(name).Result;
        }
        
        public Author? GetAuthorByEmail(string email)
        {
            return _authorRepository.FindAuthorByEmail(email).Result;
        }
        
        public Author CreateNewAuthor(int id, string name, string email, List<Cheep> cheeps)
        {
            return _authorRepository.NewAuthor(id, name, email, cheeps).Result;
        }
        
    }
}
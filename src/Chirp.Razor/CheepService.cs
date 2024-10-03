using Chirp.Razor.Repositories;

namespace Chirp.Razor
{
    public interface ICheepService
    {
        public List<CheepDto> GetCheeps(int page);
        public List<CheepDto> GetCheepsFromAuthor(string author, int page);
    }

    public class CheepService : ICheepService
    {
        private readonly ICheepRepository _cheepRepository;
        private const int PageSize = 32;

        public CheepService(ICheepRepository cheepRepository)
        {
            _cheepRepository = cheepRepository;
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
    }
}
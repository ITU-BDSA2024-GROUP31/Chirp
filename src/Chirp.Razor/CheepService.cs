namespace Chirp.Razor
{
    public interface ICheepService
    {
        public List<CheepViewModel> GetCheeps(int page);
        public List<CheepViewModel> GetCheepsFromAuthor(string author, int page);
    }

    public class CheepService : ICheepService
    {
        private readonly DBFacade _dbFacade = new DBFacade();
        private const int PageSize = 32;

        public List<CheepViewModel> GetCheeps(int page)
        {
            int pagesToSkip = (page - 1) * PageSize;
            return _dbFacade.GetCheeps().Skip(pagesToSkip).Take(PageSize).ToList();
        }

        public List<CheepViewModel> GetCheepsFromAuthor(string author, int page)
        {
            int pagesToSkip = (page - 1) * PageSize;
            return _dbFacade.GetCheepsFromAuthor(author).Skip(pagesToSkip).Take(PageSize).ToList();
        }
    }
}
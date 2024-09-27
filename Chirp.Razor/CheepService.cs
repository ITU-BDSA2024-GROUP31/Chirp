namespace Chirp.Razor
{
    public interface ICheepService
    {
        public List<CheepViewModel> GetCheeps();
        public List<CheepViewModel> GetCheepsFromAuthor(string author);
    }

    public class CheepService : ICheepService
    {
        private readonly DBFacade _dbFacade = new DBFacade();


        public List<CheepViewModel> GetCheeps()
        {
            return _dbFacade.GetCheeps();

        }

        public List<CheepViewModel> GetCheepsFromAuthor(string author)
        {
            return _dbFacade.GetCheepsFromAuthor(author);
        }

    }
}

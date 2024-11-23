namespace Chirp.Razor.Repositories
{
    public interface ICheepRepository
    {
        Task<List<CheepDto>> GetCheepsByUserName(string userName);
        Task<List<CheepDto>> ReadAllCheeps();
        Task<List<CheepDto>> AddCheepAsync(Author author, Cheep newCheep);
        Task<List<CheepDto>> ReadCheepsFromAuthor(string authorName);
        Task<List<CheepDto>> NewCheep(string text, string userName);
    }
}
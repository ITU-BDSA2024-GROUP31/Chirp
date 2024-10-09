namespace Chirp.Razor.Repositories;

public interface ICheepRepository
{
    public Task<List<CheepDto>> ReadCheepsFromAuthor(string userName);
    public Task<List<CheepDto>> ReadAllCheeps();
    public Task<List<CheepDto>> CreateNewCheep(string text, string userName);
}
namespace Chirp.Core.RepositoryInterfaces;
using Chirp.Core.Entities;
using Chirp.Core.DTOs;

public interface ICheepRepository
{
    public Task<List<CheepDto>> ReadCheepsFromAuthor(string userName);
    public Task<List<CheepDto>> ReadAllCheeps();
    public Task<List<CheepDto>> NewCheep(string text, string userName);
}
namespace Chirp.Core.RepositoryInterfaces;
using Chirp.Core.Entities;

public interface IAuthorRepository
{
    public Task<Author?> FindAuthorByName(string userName);
    public Task<Author?> FindAuthorByEmail(string email);
    public Task<Author> NewAuthor(int id, string name, string email, List<Cheep> cheeps);

    public Task<Author?> DeleteAuthorInfo(string UserName);

}
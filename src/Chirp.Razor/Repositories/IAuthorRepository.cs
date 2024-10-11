namespace Chirp.Razor.Repositories;

public interface IAuthorRepository
{
    public Task<Author> FindAuthorByName(string userName);
    public Task<Author> FindAuthorByEmail(string email);
    public Task<Author> NewAuthor(string name, string email);

}
using Chirp.Core.Entities;

namespace Chirp.Razor.Repositories;

public interface IAuthorRepository
{
    public Task<Author?> FindAuthorByName(string userName);
    public Task<Author?> FindAuthorByEmail(string email);
    public Task<Author> NewAuthor(int id, string name, string email, List<Cheep> cheeps);

    Task FollowAuthor(int followerId, int followeeId);
    Task UnfollowAuthor(int followerId, int followeeId);
    Task<Author?> FindAuthorById(int authorId);

}
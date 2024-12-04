using Chirp.Core.Entities;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Core.DTOs;
using System.Collections.Generic;

namespace Chirp.Infrastructure.Services
{
    public interface IAuthorService
    {
        Author? GetAuthorByName(string name);
        Author? GetAuthorByEmail(string email);
        Author CreateNewAuthor(int id, string name, string email, List<Cheep> cheeps);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private const int PageSize = 32;

        public AuthorService(ICheepRepository cheepRepository, IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
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
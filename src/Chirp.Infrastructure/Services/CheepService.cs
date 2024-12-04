using Chirp.Core.Entities;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Core.DTOs;
using System.Collections.Generic;
using Chirp.Razor.Repositories;

namespace Chirp.Infrastructure.Services
{
    public interface ICheepService
    {
        List<CheepDto> GetCheeps(int page);
        List<CheepDto> GetCheepsFromAuthor(string author, int page);
        List<CheepDto> CreateNewCheep(string text, string userName);
        Author? GetAuthorByName(string name);
        Author? GetAuthorByEmail(string email);
        Author CreateNewAuthor(int id, string name, string email, List<Cheep> cheeps);
    }

    public class CheepService : ICheepService
    {
        private readonly ICheepRepository _cheepRepository;
        private readonly IAuthorRepository _authorRepository;
        private const int PageSize = 32;

        public CheepService(ICheepRepository cheepRepository, IAuthorRepository authorRepository)
        {
            _cheepRepository = cheepRepository;
            _authorRepository = authorRepository;
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

        public List<CheepDto> CreateNewCheep(string text, string userName)
        {
            return _cheepRepository.NewCheep(text, userName).Result;
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
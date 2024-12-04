using Chirp.Core.Entities;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Core.DTOs;
using System.Collections.Generic;

namespace Chirp.Infrastructure.Services
{
    public interface ICheepService
    {
        List<CheepDto> GetCheeps(int page);
        List<CheepDto> GetCheepsFromAuthor(string author, int page);
        List<CheepDto> CreateNewCheep(string text, string userName);
    }

    public class CheepService : ICheepService
    {
        private readonly ICheepRepository _cheepRepository;
        private const int PageSize = 32;

        public CheepService(ICheepRepository cheepRepository, IAuthorRepository authorRepository)
        {
            _cheepRepository = cheepRepository;
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
    }
}
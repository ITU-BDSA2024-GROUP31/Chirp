using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core.Entities;
using Chirp.Core.DTOs;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Web.Pages
{
    public class UserInformationModel : PageModel
    {
        private readonly ICheepService _cheepService;
        private readonly IAuthorService _authorService;

        public string Username { get; set; }
        public string Email { get; set; }
        public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();

        public Author? Author { get; set; }

        public UserInformationModel(ICheepService cheepService, IAuthorService authorService)
        {
            _cheepService = cheepService;
            _authorService = authorService;
        }

        public async Task<IActionResult> OnGetAsync(string userName, int page = 1)
        {
            Author = _authorService.GetAuthorByName(userName);
            if (Author == null)
            {
                return NotFound();
            }

            Username = Author.Name;
            Email = Author.Email;
            Cheeps = _cheepService.GetCheepsFromAuthor(userName, page);

            return Page();
        }
    }
    }

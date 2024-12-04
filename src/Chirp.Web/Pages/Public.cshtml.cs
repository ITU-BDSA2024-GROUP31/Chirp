using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Chirp.Infrastructure.Services;
using Chirp.Core.Entities;
using Chirp.Core.RepositoryInterfaces;
using Chirp.Core.DTOs;
using Chirp.Razor.Repositories;

namespace Chirp.Web.Pages
{
    public class PublicModel : PageModel
    {
        private readonly ICheepService _service;
        private readonly UserManager<Author> _userManager;
        private readonly IAuthorRepository _authorRepository;

        public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();
        public Author? CurrentUser { get; set; }

        [BindProperty]
        [Required]
        public string Message { get; set; } = string.Empty;

        public PublicModel(ICheepService service, UserManager<Author> userManager, IAuthorRepository authorRepository)
        {
            _service = service;
            _userManager = userManager;
            _authorRepository = authorRepository;
        }

        public async Task<IActionResult> OnGetAsync([FromQuery] int page = 1)
        {
            Cheeps = _service.GetCheeps(page);
            if (User.Identity?.IsAuthenticated ?? false)
            {
                CurrentUser = await _userManager.GetUserAsync(User);
                CurrentUser = await _authorRepository.FindAuthorByName(CurrentUser.UserName);
            }
            return Page();
        }
    }
}
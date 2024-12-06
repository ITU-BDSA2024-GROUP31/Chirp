using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Core.Entities;
using Chirp.Core.DTOs;
using Chirp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
namespace Chirp.Web.Pages
{
    public class UserInformationModel : PageModel
    {
        private readonly ICheepService _cheepService;
        private readonly IAuthorService _authorService;
        private readonly SignInManager<Author> _signInManager;

        public string? Username { get; set; }
        public string? Email { get; set; }
        public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();

        public Author? Author { get; set; }

        public UserInformationModel(ICheepService cheepService, IAuthorService authorService, SignInManager<Author> signInManager)
        {
            _cheepService = cheepService;
            _authorService = authorService;
            _signInManager = signInManager;
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

        public async Task<IActionResult> OnPostAsync(string userName)
        {
            // Sign out the user
            await _signInManager.SignOutAsync();

            // Delete the user
            Author = _authorService.DeleteAuthorInfo(userName);

            if (Author == null)
            {
                // Handle case where the user is not found
                TempData["ErrorMessage"] = "User not found or already deleted.";
                return NotFound();
            }

            TempData["SuccessMessage"] = "Your account has been deleted successfully.";
            return LocalRedirect("/");
        }


    }
}

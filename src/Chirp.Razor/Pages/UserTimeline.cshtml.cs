using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Chirp.Razor.Repositories;

namespace Chirp.Razor.Pages
{
    [Authorize]
    public class UserTimelineModel : PageModel
    {
        private readonly ICheepService _service;
        private readonly IAuthorRepository _authorRepository;
        private readonly UserManager<Author> _userManager;

        public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();
        public Author? Author { get; set; }
        public Author? CurrentUser { get; set; }

        [BindProperty]
        [Required]
        public string Message { get; set; } = string.Empty;

        public UserTimelineModel(ICheepService service, IAuthorRepository authorRepository, UserManager<Author> userManager)
        {
            _service = service;
            _authorRepository = authorRepository;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(string author, [FromQuery] int page = 1)
        {
            Author = await _authorRepository.FindAuthorByName(author);
            Cheeps = _service.GetCheepsFromAuthor(author, page);
            CurrentUser = await _userManager.GetUserAsync(User);
            return Page();
        }

        public async Task<IActionResult> OnPostFollowAsync(int followeeId)
        {
            var followerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            await _authorRepository.FollowAuthor(followerId, followeeId);
            return RedirectToPage(new { author = Author?.Name });
        }

        public async Task<IActionResult> OnPostUnfollowAsync(int followeeId)
        {
            var followerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            await _authorRepository.UnfollowAuthor(followerId, followeeId);
            return RedirectToPage(new { author = Author?.Name });
        }

        public ActionResult OnPostNewCheep(string text, string userName)
        {
            Cheeps = _service.CreateNewCheep(text, userName);
            return Page();
        }

        public ActionResult OnGetByUserName(string userName)
        {
            Author = _service.GetAuthorByName(userName);
            return Page();
        }

        public ActionResult OnGetByEmail(string email)
        {
            Author = _service.GetAuthorByEmail(email);
            return Page();
        }

        public ActionResult OnPostCreateAuthor(int id, string name, string email, List<Cheep> cheeps)
        {
            Author = _service.CreateNewAuthor(id, name, email, cheeps);
            return Page();
        }
    }
}
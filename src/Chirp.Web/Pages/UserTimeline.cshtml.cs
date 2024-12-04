using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Chirp.Razor.Repositories;
using Microsoft.EntityFrameworkCore;
using Chirp.Infrastructure.Services;
using Chirp.Core.DTOs;
using Chirp.Core.Entities;

namespace Chirp.Web.Pages
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

        public async Task<IActionResult> OnGet(string author, [FromQuery] int page = 1)
        {
            // Fetch the author by their name using the repository
            Author = await _authorRepository.FindAuthorByName(author);



            // Fetch the cheeps associated with this author for the given page
            Cheeps = _service.GetCheepsFromAuthor(author, page);

            // Get the current logged-in user using UserManager
            CurrentUser = await _userManager.GetUserAsync(User);

            if (CurrentUser != null)
            {
                // Reload the Following collection to reflect the most recent changes
                CurrentUser = await _userManager.Users
                    .Include(u => u.Following)  // Include the Following navigation property
                    .ThenInclude(f => f.FolloweeAuthor)  // Optionally include FolloweeAuthor for better performance
                    .FirstOrDefaultAsync(u => u.Id == CurrentUser.Id);
            }

            return Page();
        }



        public async Task<IActionResult> OnPostFollow(int followeeId)
        {
            var followerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            if (followerId == null)
            {
                Console.WriteLine("Current user not found.");
            }

            // Call repository method to follow
            await _authorRepository.FollowAuthor(followerId, followeeId);

            // Reload CurrentUser to reflect updated "Following"
            CurrentUser = await _userManager.Users
                .Include(u => u.Following)
                .ThenInclude(f => f.FolloweeAuthor)
                .FirstOrDefaultAsync(u => u.Id == followerId);

            // Reload Author for the page
            Author = await _authorRepository.FindAuthorById(followeeId);

            // Redirect to the same page to reflect the changes
            return Redirect("/");  // Or to the current author's page
        }




        public async Task<IActionResult> OnPostUnfollow(int followeeId)
        {
            //var followerId = CurrentUser?.Id;
            var followerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "0");
            await _authorRepository.UnfollowAuthor(followerId, followeeId);
            return Redirect("/");
            //return RedirectToPage(new { author = Author?.Name });
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
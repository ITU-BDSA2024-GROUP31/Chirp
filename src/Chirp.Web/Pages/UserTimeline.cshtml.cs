using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure.Services;
using Chirp.Core.Entities;
using Chirp.Core.DTOs;
using System.ComponentModel.DataAnnotations;


namespace Chirp.Web.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _cheepService;
    private readonly IAuthorService _authorService;
    public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();
    public Author? Author { get; set; }
    
    
    [BindProperty]
    [StringLength(160)]
    public required string Message { get; set; }

    public UserTimelineModel(ICheepService cheepService, IAuthorService authorService)
    {
        _cheepService = cheepService;
        _authorService = authorService;
    }

 
    public ActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        Cheeps = _cheepService.GetCheepsFromAuthor(author, page);
        return Page();
    }

    public ActionResult OnPostNewCheep(string text, string userName)
    {
        Cheeps = _cheepService.CreateNewCheep(text, userName);
        return Page();

    }
    
    public ActionResult OnGetByUserName(string userName)
    {
        Author = _authorService.GetAuthorByName(userName);
        return Page();
    }
    
    public ActionResult OnGetByEmail(string email)
    {
        Author = _authorService.GetAuthorByEmail(email);
        return Page();
    }
    
    public ActionResult OnPostCreateAuthor(int id, string name, string email, List<Cheep> cheeps)
    {
        Author = _authorService.CreateNewAuthor(id, name, email, cheeps);
        return Page();
    }
    
    public ActionResult OnPostDeleteCheep (int cheepId)
    {
        _cheepService.DeleteCheep(cheepId);
        return RedirectToPage();
    }
}
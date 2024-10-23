using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();
    public Author? Author { get; set; }

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

 
    public ActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
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
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();

    public PublicModel(ICheepService service )
    {
        _service = service;
    }

    public ActionResult OnGet([FromQuery] int page = 1)
    {
        Cheeps = _service.GetCheeps(page);
        return Page();
    }
    public ActionResult OnPost(string Message, Author author, DateTime timestamp)
    {
        var c = new Cheep
        {
            Text = Message,
            Author = author,
            Timestamp = timestamp 
        }; 
        return RedirectToPage("/"); 
    }
}
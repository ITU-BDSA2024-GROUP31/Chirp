using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();

    [BindProperty]
    [Required]
    public string Message { get; set; }


    public PublicModel(ICheepService service )
    {
        _service = service;
    }

    public ActionResult OnGet([FromQuery] int page = 1)
    {
        Cheeps = _service.GetCheeps(page);
        return Page();
    }
    
}
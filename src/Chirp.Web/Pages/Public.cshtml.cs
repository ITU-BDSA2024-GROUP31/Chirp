using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Chirp.Infrastructure.Services;
using Chirp.Core.DTOs;


namespace Chirp.Web.Pages;

public class PublicModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();

    [BindProperty]
    [StringLength(160)]
    public required string Message { get; set; }


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
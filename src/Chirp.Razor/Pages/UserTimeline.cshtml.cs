﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chirp.Razor.Pages;

public class UserTimelineModel : PageModel
{
    private readonly ICheepService _service;
    public List<CheepDto> Cheeps { get; set; } = new List<CheepDto>();

    public UserTimelineModel(ICheepService service)
    {
        _service = service;
    }

 
    public ActionResult OnGet(string author, [FromQuery] int page = 1)
    {
        Cheeps = _service.GetCheepsFromAuthor(author, page);
        return Page();
    }
}
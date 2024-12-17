using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;


namespace Chirp.Web.Pages;

public class SubmitMessageModel : PageModel
{
    private readonly ICheepService _service;

    [BindProperty]
    [StringLength(160)]
    public required string Message { get; set; }

    public SubmitMessageModel(ICheepService service)
    {
        _service = service;
    }


    public ActionResult OnPost()
    {
        if (Message.Length > 160)
        {
            ModelState.AddModelError("Message", "Message cannot exceed 160 characters.");

        }

        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (User?.Identity?.IsAuthenticated == true)
        {
            string? name = User.Identity.Name;
            _service.CreateNewCheep(Message, name ?? string.Empty);
            return Redirect("/"); // Redirect to the Public Timeline after submitting
        }


        ModelState.AddModelError("", "You must be logged in to submit a message.");
        return Page();
    }
}
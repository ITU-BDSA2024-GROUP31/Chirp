using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Chirp.Infrastructure.Services;
using System.ComponentModel.DataAnnotations;


namespace Chirp.Web.Pages;

public class SubmitMessageModel : PageModel
{
    private readonly ICheepService _service;
    [BindProperty]
    [Required]
    public string Message { get; set; }

    public SubmitMessageModel(ICheepService service)
    {
        _service = service;
    }


    public ActionResult OnPost()
    {
      
        if (User.Identity.IsAuthenticated)
        {
            string Name = User.Identity.Name;
            _service.CreateNewCheep(Message, Name);
            return Redirect("/"); // Redirect to the Public Timeline after submitting
        }

        
        ModelState.AddModelError("", "You must be logged in to submit a message.");
        return Page(); 
    }
}
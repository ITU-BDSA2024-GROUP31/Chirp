using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chirp.Razor;

public class Author : IdentityUser<int>
{

    [StringLength(254)]
    [Required]
    public string Name { get; set; }

    [StringLength(254)]
    public string? Email { get; set; }

    public List<Cheep>? Cheeps { get; set; }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic; 
using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class Author : IdentityUser<int>
{
    [StringLength(254)]
    public required string Name { get; set; }

    [StringLength(254)]
    public string? Email { get; set; }


    public List<Cheep> Cheeps { get; set; } = new List<Cheep>();
}

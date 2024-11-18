using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class Author : IdentityUser<int>
{
    [StringLength(254)]
    [Required]
    public string Name { get; set; }

    [StringLength(254)]
    public string? Email { get; set; }


    public List<Cheep> Cheeps { get; set; } = new List<Cheep>();
    public HashSet<Author> Followers { get; set; } = new HashSet<Author>();
}

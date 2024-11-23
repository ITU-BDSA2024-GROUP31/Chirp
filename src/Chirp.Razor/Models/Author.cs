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
    public new string? Email { get; set; } 


    public List<Cheep> Cheeps { get; set; } = new List<Cheep>();
    public ICollection<Follower> Followers { get; set; } = new List<Follower>();
    public ICollection<Follower> Following { get; set; } = new List<Follower>();

}

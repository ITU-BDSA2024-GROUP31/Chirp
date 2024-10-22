using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chirp.Razor;

public class Author
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
    public int AuthorId { get; set; }
    
    [StringLength(254)]
    [Required]
    public string Name{ get; set; }
    
    [StringLength(254)]
    public string? Email{ get; set; }
    public List<Cheep>? Cheeps { get; set; }

}
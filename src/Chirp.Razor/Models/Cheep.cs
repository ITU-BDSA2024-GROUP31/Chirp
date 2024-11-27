using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class Cheep
{   
    public int CheepId  { get; set; }
    public int AuthorId {get; set;}
    
    public Author Author {get; set;}
    
    [StringLength(160)]
    public required string Text {get; set;}
    public DateTime Timestamp {get; set;}
    
}
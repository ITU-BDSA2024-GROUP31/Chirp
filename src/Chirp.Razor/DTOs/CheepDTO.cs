namespace Chirp.Razor;

public class CheepDto
{
    public string Author;
    public string Message;
    public string Timestamp;
    private string name;
    private string text;
    private DateTime timestamp;

    public HashSet<Author> Followers { get; set; }

    public CheepDto(string author, string message, DateTime timestamp, HashSet<Author> Followers)
    {
        this.Author = author;
        this.Message = message;
        this.Timestamp = ConvertDateTimeToString(timestamp);
        //this.Followers = Followers;
    }

    private string ConvertDateTimeToString(DateTime timestamp)
    {
        return timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    }

}
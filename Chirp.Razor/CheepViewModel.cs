namespace Chirp.Razor;

public class CheepViewModel
{
    public string author;
    public string message;
    public string timestamp;

    public CheepViewModel(string author, string message, string timestamp)
    {
        this.author = author;
        this.message = message;
        this.timestamp = timestamp;
    }
}
namespace Chirp.Razor;

public class CheepDto
{
    public string Author;
    public string Message;
    public string Timestamp;

    public CheepDto(string author, string message, DateTime timestamp)
    {
        this.Author = author;
        this.Message = message;
        this.Timestamp = ConvertDateTimeToString(timestamp);
    }

    private string ConvertDateTimeToString(DateTime timestamp)
    {
        return timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    }

}
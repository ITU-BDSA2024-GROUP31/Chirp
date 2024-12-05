namespace Chirp.Core.DTOs;

public class CheepDto
{
    public string Author;
    public string Message;
    public string Timestamp;
    public int CheepId;


    public CheepDto(int cheepId, string author, string message, DateTime timestamp)
    {
        this.CheepId = cheepId;
        this.Author = author;
        this.Message = message;
        this.Timestamp = ConvertDateTimeToString(timestamp);
    }

    private string ConvertDateTimeToString(DateTime timestamp)
    {
        return timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    }

}
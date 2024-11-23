namespace Chirp.Razor
{
    public record CheepDto(AuthorDto Author, string Message, string Timestamp)
    {
        public CheepDto(AuthorDto author, string message, DateTime timestamp)
            : this(author, message, ConvertDateTimeToString(timestamp))
        {
        }

        private static string ConvertDateTimeToString(DateTime timestamp)
        {
            return timestamp.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
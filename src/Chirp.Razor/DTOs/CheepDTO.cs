namespace Chirp.Razor
{
    public record CheepDto(string Author, string Message, string Timestamp, HashSet<Author> Followers)
    {
        public CheepDto(string author, string message, DateTime timestamp, HashSet<Author> followers)
            : this(author, message, ConvertDateTimeToString(timestamp), followers)
        {
        }

        private static string ConvertDateTimeToString(DateTime timestamp)
        {
            return timestamp.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
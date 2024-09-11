class UserInterface
{

    public static void Read(IEnumerable<Cheep> cheeps)
    {
        foreach (var cheep in cheeps)
        {
            var formattedTime = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp).ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"{cheep.Author} @ {formattedTime}: {cheep.Message}");
        }

    }
}
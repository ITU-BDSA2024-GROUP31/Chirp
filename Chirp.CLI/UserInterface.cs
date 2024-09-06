using SimpleDB;
class UserInterface
{

    public static void Read(CsvDatabase<Cheep> cheepDb, int limit)
    {
        try
        {
            var cheeps = cheepDb.Read(limit);
            foreach (var cheep in cheeps)
            {
                var formattedTime = DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp).ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine($"{cheep.Author} @ {formattedTime}: {cheep.Message}");
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }

    public static void Cheep(CsvDatabase<Cheep> cheepDb, string message)
    {
        try
        {
            cheepDb.Store(new Cheep(Environment.UserName, message, DateTimeOffset.Now.ToUnixTimeSeconds()));
        }
        catch (IOException e)
        {
            Console.WriteLine("The file could not be read:");
            Console.WriteLine(e.Message);
        }
    }
}
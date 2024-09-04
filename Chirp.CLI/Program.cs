using SimpleDB;

CsvDatabase<Cheep> cheepDb = new CsvDatabase<Cheep>();

if (args[0] == "read"){
   
    try
    {
        var cheeps = cheepDb.Read();
        foreach (var cheep in cheeps)
        {
            Console.WriteLine($"{cheep.Author} cheeped: {cheep.Message} at {DateTimeOffset.FromUnixTimeSeconds(cheep.Timestamp)}");
        }
    } catch (IOException e) 
    { 
        Console.WriteLine("The file could not be read:"); 
        Console.WriteLine(e.Message); 
    } 
} else if (args[0] == "cheep") { 
    try { 
        cheepDb.Store(new Cheep(Environment.UserName, args[1], DateTimeOffset.Now.ToUnixTimeSeconds()));
    } catch (IOException e) { 
        Console.WriteLine("The file could not be written:"); 
        Console.WriteLine(e.Message); 
    } catch (IndexOutOfRangeException) { 
        Console.WriteLine("Please provide a message to cheep"); 
    } 
} else { 
    Console.WriteLine("Invalid command"); 
}

public record Cheep(string Author, string Message, long Timestamp);
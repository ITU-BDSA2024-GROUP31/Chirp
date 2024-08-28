using System.Text.RegularExpressions;

try
{
    // Open the text file using a stream reader.
    using StreamReader reader = new("/Users/frederik/Desktop/Git/Chirp/Chirp.CLI/Data/chirp_cli_db.csv");
    reader.ReadLine();
    
    while (!reader.EndOfStream)
    {
        var text = reader.ReadLine();
        
        if (text != null)
        {
            string[] words = Regex.Split(text, @",(?=(?:[^""]*""[^""]*"")*[^""]*$)");
            if (words.Length > 2)
            {
                var date = DateTimeOffset.FromUnixTimeSeconds(long.Parse(words[2]));
                var formattedDate = date.ToString("dd/MM/yyyy HH:mm:ss");
                Console.WriteLine(words[0] + " @ " + formattedDate + ": " + words[1]);
            }
            
        }
        
    }
    
}
catch (IOException e)
{
    Console.WriteLine("The file could not be read:");
    Console.WriteLine(e.Message);
}


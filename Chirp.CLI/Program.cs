using System.Text.RegularExpressions;
using DocoptNet;

const string usage = @"Chirp CLI version.

Usage:
  chirp read <limit>
  chirp cheep <message> 
  chirp (-h | --help)
  chirp --version

Options:
  -h --help     Show this screen.
  --version     Show version.
";

var arguments = new Docopt().Apply(usage, args, version: "0.1", exit: true)!;

    //check if "read" appears
// run read by typing: dotnet run -- read <limit>
if(arguments["read"].IsTrue)

{
    
    try
    {
        // Open the text file using a stream reader.
        using StreamReader reader = new("Data/chirp_cli_db.csv");
        reader.ReadLine();

        // Checks weather the SyreamReader has reached the end of the file
        while (!reader.EndOfStream)
        {
            // Reads each line
            var text = reader.ReadLine();

            if (text != null)
            {
                /* We split the line we are reading on the commas excluding the commas inside ""
                Example: "Hello, World" the comma inside would not be considered*/
                string[] words = Regex.Split(text, @",(?=(?:[^""]*""[^""]*"")*[^""]*$)");

                if (words.Length > 2)
                {
                    /* We convert the given timestap from UnixTimeSeconds to a DateTimeOffset object and also format it
                    to a standart format*/
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
    // to make a cheep write: dotnet run -- cheep"xyz"
} else if(arguments["cheep"].IsTrue) //check if cheep appears
{
    try
    {
        // Using StreamWriter we give it the csv file and set the appned to true so we can append to the file
        using StreamWriter writer = new("Data/chirp_cli_db.csv", append: true);
        writer.WriteLine(Environment.UserName + ",\"" + args[1] + "\"," + DateTimeOffset.Now.ToUnixTimeSeconds());
    }
    catch (IOException e)
    {
        Console.WriteLine("The file could not be written:");
        Console.WriteLine(e.Message);
    }
}
else
{
    Console.WriteLine("Invalid command");
}




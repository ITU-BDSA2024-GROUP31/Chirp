using System.Diagnostics;

namespace PlaywrightTests;

/*
 * This util class is implemented from
 * https://github.com/ITU-BDSA2024-GROUP21/Chirp/blob/main/test/Chirp.Razor.Tests/PlaywrightTests/ServerUtil.cs
 *
 * Credit: Group 21, ITU BDSA 2024 Course
 */

public static class TestingUtilForServer
{
    public static async Task<Process> StartServer()
    {
        string projectPath = @"C:\Users\denni\OneDrive - ITU\GitHub\TwitterAnalysisDesign\Chirp\src\Chirp.Web\Chirp.Web.csproj";

        if (!File.Exists(projectPath))
        {
            throw new FileNotFoundException($"Project file not found at {projectPath}");
        }

        Console.WriteLine($"Resolved Project Path: {projectPath}");

        var serverProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = $"run --project \"{projectPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        /*serverProcess.OutputDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                Console.WriteLine("Output: " + args.Data);
            }
        };

        serverProcess.ErrorDataReceived += (sender, args) =>
        {
            if (args.Data != null)
            {
                Console.WriteLine("Error: " + args.Data);
            }
        };*/

        serverProcess.Start();
        serverProcess.BeginOutputReadLine();
        serverProcess.BeginErrorReadLine();

        await Task.Delay(1000); // Give the server time to start.
        return serverProcess;
    }



}
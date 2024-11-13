using System.Diagnostics;
using System.Net.Http;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;
[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    private Process _serverProcess;

    [OneTimeSetUp]
    public async Task StartServer()
    {
        _serverProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run --project \"C:/Users/denni/OneDrive - ITU/GitHub/TwitterAnalysisDesign/Chirp/src/Chirp.Razor/Chirp.Razor.csproj\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        
        /*_serverProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run --project \"C:/Users/denni/OneDrive - ITU/GitHub/TwitterAnalysisDesign/Chirp/src/Chirp.Razor/Chirp.Razor.csproj\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };*/

        _serverProcess.Start();
    }
    
    
    
    
    [OneTimeTearDown]
    public void StopServer()
    {
        if (!_serverProcess.HasExited)
        {
            _serverProcess.Kill();
            _serverProcess.Dispose();
        }
    }
    
    // Minimize redundancy for now while we don't have a server to reset the database
    private async Task LoginHelper()
    {
        await Page.GotoAsync("http://localhost:5273/Account/Login");

        // Logging in with the user we registered
        await Page.GetByPlaceholder("name@example.com").ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("Test123@itu.dk");
        await Page.GetByPlaceholder("password").ClickAsync();
        await Page.GetByPlaceholder("password").FillAsync("Test!123");
        await Page.GetByLabel("Remember me?").CheckAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }
    
    // Minimize redundancy for now while we don't have a server to reset the database
    private async Task LogginoutHelper()
    {
        var logoutLink = Page.GetByRole(AriaRole.Link, new() { Name = "Logout [Test123@itu.dk]" });
        if (await logoutLink.IsVisibleAsync())
        {
            await logoutLink.ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();
        }
    }
    
    
    [Test]
    public async Task TestStartPage()
    {
        await Page.GotoAsync("http://localhost:5273");
        // Expect a title of string "Chirp".
        await Expect(Page).ToHaveTitleAsync("Chirp!");
        
        // Expect that our navigation bar has Public Timeline, Register, and Login.
        await Expect(Page.GetByText("Public Timeline | Register | Login")).ToBeVisibleAsync();
        
        // Expect not visible since we haven't logged in.
        await Expect(Page.Locator("My Timeline")).Not.ToBeVisibleAsync();
        await Expect(Page.Locator("Logout")).Not.ToBeVisibleAsync();
    }
    
    // Navigation bar tests
    [Test]
    public async Task TestClickingPublicTimeLineNav()
    {
        await Page.GotoAsync("http://localhost:5273");
        
        // Expect that our navigation bar has Public Timeline, Register, and Login.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" }).ClickAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline" })).ToBeVisibleAsync();

        await Expect(Page).ToHaveURLAsync(new Regex("http://localhost:5273/"));
        
    }
    
    
    [Test]
    public async Task TestClickingRegisterNav()
    {
        await Page.GotoAsync("http://localhost:5273");
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Create a new account." })).ToBeVisibleAsync();
        
        await Expect(Page.GetByPlaceholder("Your Name")).ToBeVisibleAsync();
        
        await Expect(Page.GetByPlaceholder("name@example.com")).ToBeVisibleAsync();
        
        await Expect(Page.GetByLabel("Password", new() { Exact = true })).ToBeVisibleAsync();
        
        await Expect(Page.GetByLabel("Confirm Password")).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Register" })).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use another service to" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
        
    }
    
    
    [Test]
    public async Task TestClickingLogInNav()
    {
        await Page.GotoAsync("http://localhost:5273");

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Log in", Exact = true })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use a local account to log in." })).ToBeVisibleAsync();
        
        await Expect(Page.GetByPlaceholder("name@example.com")).ToBeVisibleAsync();
        
        await Expect(Page.GetByPlaceholder("password")).ToBeVisibleAsync();
        
        await Expect(Page.GetByText("Remember me?")).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Log in" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Forgot your password?" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register as a new user" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Resend email confirmation" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use another service to log in." })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
    }
    
    
    // Registering a dummy user to use for login test and future tests
    /*[Test]
    public async Task TestRegistering()
    {
        await Page.GotoAsync("http://localhost:5273/Account/Register");
        
        await Page.GetByPlaceholder("Your Name").ClickAsync();
        await Page.GetByPlaceholder("Your Name").FillAsync("TheTester");
        await Page.GetByPlaceholder("name@example.com").ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("Test123@itu.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Test!123");
        await Page.GetByLabel("Confirm Password").ClickAsync();
        await Page.GetByLabel("Confirm Password").FillAsync("Test!123");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        // Expect 
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Logout [Test123@itu.dk]" })).ToBeVisibleAsync();
        
    }*/
    
    [Test]
    public async Task TestingIfWeSuccefullyLoggedInByCheckingUI()
    {
        await LoginHelper();
        
        // Expecting new elements since we are logged in
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Logout [Test123@itu.dk]" })).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind?" })).ToBeVisibleAsync();
        
        await Expect(Page.Locator("form")).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Share" })).ToBeVisibleAsync();
    }
    
    
    /*[Test]
    public async Task TestCheepsInMyTimeLineAndPublic()
    {
        await LoginHelper();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        await Page.Locator("#Message").ClickAsync();
        await Page.Locator("#Message").FillAsync("2 hours in the gym #GymLife #GettingStrong\ud83d\udcaa");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        
        await Expect(Page.Locator("#messagelist")).ToContainTextAsync("Test123@itu.dk 2 hours in the gym #GymLife #GettingStrong💪 — 2024-11-12 21:37:13");
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" }).ClickAsync();
        
        await Expect(Page.Locator("#messagelist")).ToContainTextAsync("Test123@itu.dk 2 hours in the gym #GymLife #GettingStrong💪 — 2024-11-12 21:37:13");
        
    }*/
    
    
    
    [Test]
    public async Task TestAccountRecorvering()
    {
        
        
    }
    
}
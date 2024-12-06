using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace PlaywrightTests;

[Parallelizable(ParallelScope.None)]
[TestFixture]
public class PlaywrightTests : PlaywrightSetupTearDownUtil
{
    // Test the front page of the application by checking what elements are visible
    [Test, Category("Playwright")]
    public async Task TestStartPage()
    {
        await Page.GotoAsync("http://localhost:5273/");
        
        // Expect a title of string "Chirp".
        await Expect(Page.Locator("h1")).ToContainTextAsync("Chirp!");
        
        // Expect that our navigation bar has Public Timeline, Register, and Login.
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Login" })).ToBeVisibleAsync();
        
        // Expect not visible since we haven't logged in.
        await Expect(Page.Locator("My Timeline")).Not.ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestChirp]" })).Not.ToBeVisibleAsync();
        


    }
    
    
    
    // Test the public timeline navigation link 
    [Test, Category("Playwright")]
    public async Task TestClickingPublicTimeLineNav()
    {
        await Page.GotoAsync("http://localhost:5273/");
        
        // Navigating to public timeline.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" }).ClickAsync();
        
        // Expect Public Timeline heading to be visible.
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Public Timeline" })).ToBeVisibleAsync();
        
        // Expect Public Timeline url 
        await Expect(Page).ToHaveURLAsync(new Regex("http://localhost:5273/"));
        
    }
    
    
    [Test, Category("Playwright")]
    public async Task TestClickingRegisterNav()
    {
        
        await Page.GotoAsync("http://localhost:5273/");
        
        // Navigating to the register page.
        await Page.GetByRole(AriaRole.Link, new() { Name = "Register" }).ClickAsync();
        
        // Expect the page to contain heading Create a new account register and other elements
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Register", Exact = true })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Create a new account." })).ToBeVisibleAsync();
        
        await Expect(Page.Locator("#registerForm div").Filter(new() { HasText = "Name" })).ToBeVisibleAsync();
        
        await Expect(Page.Locator("#registerForm div").Filter(new() { HasText = "Email" })).ToBeVisibleAsync();
        
        await Expect(Page.Locator("#registerForm div").Nth(2)).ToBeVisibleAsync();
        
        await Expect(Page.Locator("#registerForm div").Filter(new() { HasText = "Confirm Password" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Register" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use another service to" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();

        
    }
    
    
    [Test, Category("Playwright")]
    public async Task TestClickingLogInNav()
    {
        await Page.GotoAsync("http://localhost:5273/");
        
        // Navigate to login page

        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        
        
        // Expect to be visible since we are on the login page
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Log in", Exact = true })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use a local account to log in." })).ToBeVisibleAsync();
        
        await Expect(Page.Locator("#account div").Filter(new() { HasText = "Username" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByPlaceholder("password")).ToBeVisibleAsync();
        
        await Expect(Page.GetByText("Remember me?")).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Use another service to log in." })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "GitHub" })).ToBeVisibleAsync();
        
    }
    
    
    /*
     * Registering a dummy user then using forget me to delete the user so we can reuse
     * the same username, email and password in the test
     */
    [Test, Category("Playwright")]
    public async Task TestRegistering()
    {
        
        await Page.GotoAsync("http://localhost:5273/Account/Register");
        
        // Fill out register form
        await Page.GetByPlaceholder("Your Name").ClickAsync();
        await Page.GetByPlaceholder("Your Name").FillAsync("Debug");
        await Page.GetByPlaceholder("name@example.com").ClickAsync();
        await Page.GetByPlaceholder("name@example.com").FillAsync("Debug@itu.dk");
        await Page.GetByLabel("Password", new() { Exact = true }).ClickAsync();
        await Page.GetByLabel("Password", new() { Exact = true }).FillAsync("Debug123!");
        await Page.GetByLabel("Confirm Password").ClickAsync();
        await Page.GetByLabel("Confirm Password").FillAsync("Debug123!");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Register" }).ClickAsync();
        
        // Expect to be visible since we registered
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Logout [Debug]" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind, Debug?" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Share" })).ToBeVisibleAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { Name = "User Information" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Stored information about you." })).ToBeVisibleAsync();
        await Expect(Page.GetByText("Username: Debug")).ToBeVisibleAsync();
        await Expect(Page.GetByText("Email: Debug@itu.dk")).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Forget Me" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Your cheeps" })).ToBeVisibleAsync();

        /* Clicking forget me button to delete the user so when we run dotnet test again and be able to reuse the same
         * username, email and password
         */
        await Page.GetByRole(AriaRole.Button, new() { Name = "Forget Me" }).ClickAsync();



    }
    
    
    [Test, Category("Playwright")]
    public async Task TestingIfWeSuccefullyLoggedInByCheckingUi()
    {
        await LoginHelper();
        
        // Expecting new elements since we are logged in
        // Public timeline
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestChirp]" })).ToBeVisibleAsync();

        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind?" })).ToBeVisibleAsync();
        
        // User timeline
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();

        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "TestChirp's Timeline" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "What's on your mind, TestChirp?" })).ToBeVisibleAsync();
        
        await Expect(Page.Locator("#Message")).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Button, new() { Name = "Share" })).ToBeVisibleAsync();
    }
    
    [Test, Category("Playwright")]
    public async Task TestingUnsuccessfulLogin()
    {
        await Page.GotoAsync("http://localhost:5273/Account/Login");
        
        // Fill out log in information with incorrect information 
        await Page.GetByPlaceholder("Username").ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("Debuging");
        await Page.GetByPlaceholder("password").ClickAsync();
        await Page.GetByPlaceholder("password").FillAsync("Debug123tothe4!");
        await Page.GetByLabel("Remember me?").CheckAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
        
        
        // Expect error mesage to be visible 
        await Expect(Page.GetByText("Invalid login attempt.")).ToBeVisibleAsync();

    }
    
    [Test, Category("Playwright")]
    public async Task TestingLogginOut()
    {
        // Use helpers to log ing and out
        await LoginHelper();
        await LogOutHelper();

        // Expect to be visible since we are logged out
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Login" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register", Exact = true })).ToBeVisibleAsync();

    }
    
     // We need to delete the cheep from the database after each test to be able to reuse the same cheep
    
    [Test, Category("Playwright")]
    public async Task TestCheepsInMyTimeLineAndPublicAndDelete()
    {
        await LoginHelper();
        
        // Posting a message and sharing it
        await Page.Locator("#Message").ClickAsync();
        await Page.Locator("#Message").FillAsync("I dont like playwright");
        
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        
        // Expect the cheep to be visible in the public timeline
        await Expect(Page.GetByText("TestChirp I dont like playwright")).ToBeVisibleAsync();
        
        // Navigating to my timeline and checking if the cheep is there
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        
        await Expect(Page.GetByText("TestChirp I dont like playwright")).ToBeVisibleAsync();
        
        // Deleting the cheep
        await Page.Locator("li").Filter(new() { HasText = "TestChirp I dont like playwright" }).GetByRole(AriaRole.Button).ClickAsync();


    }
}
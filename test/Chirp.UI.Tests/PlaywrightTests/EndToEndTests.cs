using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;


namespace PlaywrightTests;

[Parallelizable(ParallelScope.None)]
[TestFixture]

public class EndToEndTests : PlaywrightSetupTearDownUtil
{
    [Test, Category("Playwright")]
    public async Task End2EndTest()
    {
        await Page.GotoAsync("http://localhost:5273/");

        // Login in to the application
        await Page.GetByRole(AriaRole.Link, new() { Name = "Login" }).ClickAsync();
        await Page.GetByPlaceholder("Username").ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("TestChirp");
        await Page.GetByPlaceholder("password").ClickAsync();
        await Page.GetByPlaceholder("password").FillAsync("TestChirp123!");
        await Page.GetByLabel("Remember me?").CheckAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();

        // Posting a message and sharing it
        await Page.Locator("#Message").ClickAsync();
        await Page.Locator("#Message").FillAsync("I am end2end testing");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();

        // Navigating to my timeline and deleting the message
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        await Page.Locator("li").Filter(new() { HasText = "TestChirp I am end2end testing" }).GetByRole(AriaRole.Button)
            .ClickAsync();
        
        // Navigating to the public timeline and user information and logging out
        await Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "User Information" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestChirp]" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();
    }
}
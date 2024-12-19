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
        await Page.Locator("#Message").FillAsync("Hi there, I am end2end testing");
        await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        
        await Expect(Page.GetByText("Hi there, I am end2end testing").First).ToBeVisibleAsync();

        
        // Navigating to my timeline and deleting the message
        await Page.GetByRole(AriaRole.Link, new() { Name = "My Timeline" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "TestChirp's Profile" })).ToBeVisibleAsync();
        await Expect(Page.GetByText("Hi there, I am end2end testing").First).ToBeVisibleAsync();
        
        // Deleting the message
        await Page.GetByRole(AriaRole.Button, new() { Name = "Delete" }).ClickAsync();
         await Expect(Page.GetByText("There are no cheeps so far.")).ToBeVisibleAsync();
        
        // Navigating to the public timeline and user information and logging out
        await Page.GetByRole(AriaRole.Link, new() { Name = "Public Timeline" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "User Information" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestChirp]" }).ClickAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();
        
        
        // await Expect(Page.GetByText("Hi there, I am end2end testing").First).ToBeVisibleAsync();
        // await Expect(Page.GetByText("Playwright is something else!")).ToBeVisibleAsync();
        // await Page.Locator("li").Filter(new() { HasText = "TestChirp — 2024-12-19 06:35:" }).GetByRole(AriaRole.Button).ClickAsync();
    }
}
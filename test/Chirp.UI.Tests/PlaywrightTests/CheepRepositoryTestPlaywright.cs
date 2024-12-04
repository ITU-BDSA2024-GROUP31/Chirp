using Microsoft.Playwright;

namespace PlaywrightTests;

public class CheepRepositoryTestPlaywright : PlaywrightSetupTearDownUtil
{
    [Test, Category("Playwright")]
    public async Task TestIfaCheepEnteredInToCheepboxIsStoredInTheDb()
    {
        // NOT THE FINAL TEST JUST WANTED TO TEST OUT IF WE COULD HAVE TWO PLAYWRIGHT TESTS THIS AND IN THE END2END
        await LoginHelper();
        await LogOutHelper();

        // Expect to be visible since we are logged out
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Login" })).ToBeVisibleAsync();
        
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Register", Exact = true })).ToBeVisibleAsync();

        // need to test out if the db has the message we entered in the cheepbox
        
        // // Filling the cheepbox
        // string cheepMessage = "I am a Cheep beep boop. Am I in the Database?";
        // await Page.Locator("#Message").FillAsync(cheepMessage);
        //
        // await Page.GetByRole(AriaRole.Button, new() { Name = "Share" }).ClickAsync();
        //
        // using var scope = _serviceProvider.CreateScope();
        // var context = scope.ServiceProvider.GetRequiredService<ChatDbContext>();
        // var author = await context.Authors.Include(a => a.Cheeps)
        //     .FirstOrDefaultAsync(a => a.Email == "Debug123@itu.dk");

        
    }
    
}
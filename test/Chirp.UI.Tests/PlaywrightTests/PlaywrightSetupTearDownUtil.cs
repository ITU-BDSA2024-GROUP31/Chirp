﻿using System.Diagnostics;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.None)]
[TestFixture]

/*
 * Setup and Cleanup methods are adapted from the following source:
 * https://github.com/ITU-BDSA2024-GROUP21/Chirp/blob/main/test/Chirp.Razor.Tests/PlaywrightTests/UIEnd2EndTest.cs
 *
 * The cleanup method is modified to also kill any "dotnet" processes that may be running after the test is executed.
 *
 * Credit: Group 21, ITU BDSA 2024 Course
 */ 

public class PlaywrightSetupTearDownUtil : PageTest
{
    private Process _serverProcess;
    private IBrowser _browser;
    
    public override BrowserNewContextOptions ContextOptions()
    {
        return new BrowserNewContextOptions
        {
            IgnoreHTTPSErrors = true
        };
    }
    
    
    // TODO: Add a method to reset the database after each test and make the databse work for some reason it dosnt work
    [SetUp]
    public async Task Setup()
    {
        _serverProcess = await TestingUtilForServer.StartServer();
        Thread.Sleep(800); // Increase this if needed

        _browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions {Headless = true});
    }
    
    
    [TearDown]
    public async Task Cleanup()
    {
        if (!_serverProcess.HasExited)
        {
            _serverProcess.Kill(true); // Forcefully kill the process and child processes.
            await _serverProcess.WaitForExitAsync();
            
            await _browser.DisposeAsync();

            _serverProcess.Dispose();
        }
        
    }
    
    // Minimize redundancy
    protected async Task LoginHelper()
    {
        await Page.GotoAsync("http://localhost:5273/Account/Login");

        // Logging in with the user we registered
        await Page.GetByPlaceholder("Username").ClickAsync();
        await Page.GetByPlaceholder("Username").FillAsync("TestChirp");
        await Page.GetByPlaceholder("password").ClickAsync();
        await Page.GetByPlaceholder("password").FillAsync("TestChirp123!");
        await Page.GetByLabel("Remember me?").CheckAsync();
        await Page.GetByRole(AriaRole.Button, new() { Name = "Log in" }).ClickAsync();
    }
    
    
    protected async Task LogOutHelper()
    {
        var logoutLink = Page.GetByRole(AriaRole.Link, new() { Name = "Logout [TestChirp]" });
        if (await logoutLink.IsVisibleAsync())
        {
            await logoutLink.ClickAsync();
            await Page.GetByRole(AriaRole.Button, new() { Name = "Click here to Logout" }).ClickAsync();
        }
    }
    
}
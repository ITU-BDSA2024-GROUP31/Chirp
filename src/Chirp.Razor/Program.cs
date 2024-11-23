using Chirp.Razor;
using Chirp.Razor.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}

// Add services to the container.
builder.Services.AddRazorPages();

// Register your services
builder.Services.AddScoped<ICheepService, CheepService>();
builder.Services.AddScoped<ICheepRepository, CheepRepository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();

// Configure the DbContext to use SQLite and Identity
builder.Services.AddDbContext<ChatDbContext>(options =>
    options.UseSqlite(connectionString));

// Add Identity services
builder.Services.AddDefaultIdentity<Author>(options =>
        options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ChatDbContext>().AddDefaultTokenProviders();

builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(30);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});

// Configure the default service provider
builder.Host.UseDefaultServiceProvider(p =>
{
    p.ValidateScopes = true;
    p.ValidateOnBuild = true;
});

builder.Services.AddAuthentication()
    .AddGitHub(o =>
    {
        o.ClientId = builder.Configuration["authentication_github_clientId"];
        o.ClientSecret = builder.Configuration["authentication_github_clientSecret"];
        o.CallbackPath = "/signin-github";
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    using var context = services.GetRequiredService<ChatDbContext>();

    var userManager = services.GetRequiredService<UserManager<Author>>();

    // Ensure the database is created and apply migrations
    context.Database.Migrate();

    // Seed the database
    await DbInitializer.SeedDatabase(context, userManager);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
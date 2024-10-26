using Chirp.Razor;
using Chirp.Razor.Repositories;
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

// Configure the default service provider
builder.Host.UseDefaultServiceProvider(p =>
{
    p.ValidateScopes = true;
    p.ValidateOnBuild = true;
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    using var context = services.GetRequiredService<ChatDbContext>();

    var userManager = services.GetRequiredService<UserManager<Author>>();

    Console.WriteLine("usermanager: " + userManager);

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

// Add authentication and authorization middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
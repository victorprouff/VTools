using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VTools.App.Components;
using VTools.App.Data;
using VTools.App.Services;

var builder = WebApplication.CreateBuilder(args);

// EF Core + SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Business services
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<LentItemService>();
builder.Services.AddSingleton<LoginAttemptTracker>();

// Cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.LogoutPath = "/account/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Razor components (Blazor Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Ensure DB is created and migrations applied
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

// Login endpoint
app.MapPost("/account/login", async (HttpContext ctx, string? returnUrl) =>
{
    var tracker = ctx.RequestServices.GetRequiredService<LoginAttemptTracker>();
    var ip = ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown";

    if (tracker.IsLockedOut(ip))
    {
        var until = tracker.GetLockoutExpiryUnix(ip);
        return Results.Redirect($"/login?error=locked&until={until}");
    }

    var form = await ctx.Request.ReadFormAsync();
    var username = form["username"].ToString();
    var password = form["password"].ToString();

    var configuredUsername = Environment.GetEnvironmentVariable("AUTH_USERNAME")
        ?? ctx.RequestServices.GetRequiredService<IConfiguration>()["Auth:Username"]
        ?? "admin";
    var configuredPassword = Environment.GetEnvironmentVariable("AUTH_PASSWORD")
        ?? ctx.RequestServices.GetRequiredService<IConfiguration>()["Auth:Password"]
        ?? "changeme";

    if (username == configuredUsername && password == configuredPassword)
    {
        tracker.RecordSuccess(ip);
        var claims = new List<Claim> { new(ClaimTypes.Name, username) };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);
        await ctx.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        return Results.Redirect(returnUrl ?? "/books");
    }

    tracker.RecordFailure(ip);
    return Results.Redirect("/login?error=1");
});

// Logout endpoint
app.MapPost("/account/logout", async (HttpContext ctx) =>
{
    await ctx.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
}).RequireAuthorization();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

using Dapper;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using VTools.BookEntity;
using VTools.Components;
using VTools.Components.Account;
using VTools.Data;
using VTools.Data.Handlers;
using VTools.Data.Repositories;
using VTools.Data.Repositories.Interfaces;
using VTools.Extension;
using VTools.LoanAggregate;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBlazorBootstrap();

// Add services to the container.
builder.Services
    .ConfigureServices(builder.Configuration)
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

DefaultTypeMap.MatchNamesWithUnderscores = true;
SqlMapper.AddTypeHandler(new InstantHandler());

builder.Services.AddNpgsqlDataSource(
    connectionString,
    builder => builder.UseNodaTime());

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddHttpClient();

// builder.Services.AddTransient<IUserRepository, UserRepository>(_ =>
//     new UserRepository(connectionString));
builder.Services.AddTransient<ILoanRepository, LoanRepository>(_ =>
    new LoanRepository(connectionString));
builder.Services.AddTransient<IBookRepository, BookRepository>(_ =>
    new BookRepository(connectionString));
// builder.Services.AddTransient<IBadDayPostRepository, BadDayPostRepository>(_ =>
//     new BadDayPostRepository(connectionString));

// builder.Services.AddTransient<IUserDomain, UserDomain>();
builder.Services.AddTransient<ILoanDomain, LoanDomain>();
builder.Services.AddTransient<IBookDomain, BookDomain>();
// builder.Services.AddTransient<IBadDayPostDomain, BadDayPostDomain>();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// builder.Services.AddAuthorizationCore(options =>
//     options.AddPolicy("EditUser", policy =>
//         policy.RequireAssertion(context =>
//         {
//             if (context.Resource is RouteData rd)
//             {
//                 rd.Values.TryGetValue("id", out var value);
//                 // var routeValue = rd.RouteValues.TryGetValue("id", out var value);
//
//                 var id = Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty;
//
//                 if (!string.IsNullOrEmpty(id))
//                 {
//                     return id.StartsWith("EMP", StringComparison.InvariantCulture);
//                 }
//             }
//
//             return false;
//         }))
//     );


var app = builder.Build();

if (builder.Environment.IsEnvironment("Local"))
{
    StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevLiftNew.Models;
using DevLiftNew.Data;
using Microsoft.AspNetCore.Antiforgery;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Identity Configuration
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
});

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Data/devlift.db"));

// Antiforgery Service hinzufügen
builder.Services.AddAntiforgery(options =>
{
    options.HeaderName = "X-XSRF-TOKEN"; // Optional: Anpassen des Header-Namens für das Token
});

var app = builder.Build();

// Pipeline Configuration
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Wichtig: Reihenfolge beibehalten!
app.UseAuthentication();
app.UseAuthorization();

// Antiforgery Middleware aktivieren
app.Use(next => context =>
{
    var antiforgery = context.RequestServices.GetRequiredService<IAntiforgery>();
    antiforgery.SetCookieTokenAndHeader(context);
    return next(context);
});

app.MapRazorPages();

app.Run();
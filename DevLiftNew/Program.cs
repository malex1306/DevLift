using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DevLiftNew.Models;
using DevLiftNew.Data;  // Füge diese using-Direktion hinzu

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Identity Configuration
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=Data/devlift.db"));

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

// Wichtig: Diese beiden Middlewares in genau dieser Reihenfolge!
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();



app.Run();
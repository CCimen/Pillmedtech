using PillMedTech.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IPillMedTechRepository, EFPillMedTechRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddDbContext<LoggDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("LoggConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();


// checklista 3.6. Implementera l�senordspolicy (l�ngd och komplexitet)
// checklista 7.1. Kommentera vad som ska bort eller f�r�ndras innan drifts�ttningen
builder.Services.Configure<IdentityOptions>(options =>
{
    //# OWASP ID 2.1.1 // Enligt lösenordspolicy behöver användaren endast ha ett lösenord på minst 12 tecken
    options.Password.RequiredLength = 12;
    //OWASP ID 2.1.4 // Tillåt användaren att ha ett unicode-lösenord enligt lösenordspolicy
    options.Password.RequireNonAlphanumeric = false;
    //checlista 3.4 ett försök
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(10);
    options.Lockout.MaxFailedAccessAttempts = 3;
    // Tiden innan användaren blir utelåst
    options.Lockout.AllowedForNewUsers = true;
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    DBInitializer.EnsurePopulated(services);
    IdentityInitializer.EnsurePopulated(services).Wait();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
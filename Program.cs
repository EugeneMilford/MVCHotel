using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure database context for hotel data
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelContext")
        ?? throw new InvalidOperationException("Connection string 'HotelContext' not found.")));

// Configure database context for Identity
builder.Services.AddDbContext<HotelIdentityContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelIdentityContext")
        ?? throw new InvalidOperationException("Connection string 'HotelIdentityContext' not found.")));

// Add Identity services with the default settings
builder.Services.AddDefaultIdentity<HotelUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<HotelIdentityContext>();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    SeedData.Initialize(services);
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Removed SeedData code

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Ensure authentication middleware runs before authorization
app.UseAuthentication();
app.UseAuthorization();

// Map routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Welcome}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
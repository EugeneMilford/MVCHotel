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
builder.Services.AddIdentity<HotelUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<HotelIdentityContext>() // Use HotelIdentityContext for Identity storage
.AddDefaultTokenProviders();

// Configure Role Manager
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
});

// Add DataSeeder as a service
builder.Services.AddScoped<RoleSeeder>();

// Add services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Run migrations for both HotelContext and HotelIdentityContext
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    // Run migrations for HotelContext
    var hotelContext = services.GetRequiredService<HotelContext>();
    hotelContext.Database.Migrate();

    // Run migrations for HotelIdentityContext
    var identityContext = services.GetRequiredService<HotelIdentityContext>();
    identityContext.Database.Migrate();

    // Initialize role and user data
    var roleSeeder = services.GetRequiredService<RoleSeeder>();
    await roleSeeder.SeedAsync(); // Seed roles and users
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using HotelManagement.Data;
using HotelManagement.Models;
using HotelManagement.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelContext") ?? throw new InvalidOperationException("Connection string 'HotelContext' not found.")));

builder.Services.AddDefaultIdentity<HotelUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<HotelIdentityContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope()) 
{
    var services = scope.ServiceProvider;
    SeedData.Initialize(services);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Welcome}/{action=Index}/{id?}");

app.Run();

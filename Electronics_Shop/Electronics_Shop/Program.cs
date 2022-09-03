using Electronics_Shop.Data;
using Electronics_Shop.Properties;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// configure the DB connection and inject it
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//inject the configuration settings for later mirroring the data from app.settings
builder.Services.AddSingleton(builder.Configuration.GetSection("ConfigrationSettings").Get<ConfigrationSettings>());

builder.Services.AddControllersWithViews();

//enable cookies for handling the login for demo purpose
//also sessions will be used for shopping cart
builder.Services.AddAuthentication("Cookies").AddCookie();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Lax
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Appmovie.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppMovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppmovieContext") ?? throw new InvalidOperationException("Connection string 'AppmovieContext' not found.")));

builder.Services.AddDbContext<AppmovieIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AppmovieIdentityDbContext") ?? throw new InvalidOperationException("Connection string 'AppmovieContext' not found.")));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<AppmovieIdentityDbContext>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;
 
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();

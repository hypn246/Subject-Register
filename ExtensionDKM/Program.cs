using ExtensionDKM.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyDBContext>(options
    =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Index"; 
        options.AccessDeniedPath = "/Home/Error";
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();//auth
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "users",
    pattern: "{controller=Users}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "majors",
    pattern: "{controller=Majors}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapControllerRoute(
    name: "login",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "courses",
    pattern: "{controller=Courses}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "classes",
    pattern: "{controller=Classrooms}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "enroll",
    pattern: "{controller=Enroll}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "tkb",
    pattern: "{controller=TKB}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();

using ExtensionDKM.Data;
using ExtensionDKM.DAL;
using ExtensionDKM.BUS;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//services
builder.Services.AddControllersWithViews();

//dbContext
builder.Services.AddDbContext<MyDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));

//DAL
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IMajorRepository, MajorRepository>();
builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

//BUS
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IMajorService, MajorService>();
builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<IRoomService, RoomService>();

// Add authentication
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

using Microsoft.EntityFrameworkCore;
using AugnosBlog.Models;

// Loads environmental variables needed to connect MySQL on Railway
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession();

// Connection string to link DB without .env
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<MyContext>(options => options
//     .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var connectionString = ConnectionHelper.GetConnectionString();
builder.Services.AddDbContext<MyContext>(options => options
    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

var app = builder.Build();

// run migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MyContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/404");
}
app.UseSession();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();

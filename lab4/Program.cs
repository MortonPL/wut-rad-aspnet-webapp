using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

using lab4.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string conn = "Server=localhost;Database=ntr;User=app;Password=maslo";
builder.Services.AddDbContext<lab4.Entities.StorageContext>(
    options => options.UseMySql(conn, ServerVersion.AutoDetect(conn))
);
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html");

using (var context = new lab4.Entities.StorageContext())
{
    if (!context.Database.CanConnect())
    {
        throw new Exception("Cannot connect to the database!");
    } else {
        var pdbe = new PopulateDBEntity();
        pdbe.Populate(context);
    }
}

app.Run();

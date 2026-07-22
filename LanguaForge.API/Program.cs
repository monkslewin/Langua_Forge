using Microsoft.EntityFrameworkCore;
using LanguaForge.API.Data;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration
        .GetConnectionString("DefaultConnection")
    )
);


builder.Services.AddControllers();


var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await DbSeeder.SeedAsync(db);
}


app.UseHttpsRedirection();

app.MapControllers();


app.Run();
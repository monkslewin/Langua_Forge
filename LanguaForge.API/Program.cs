using Microsoft.EntityFrameworkCore;
using LanguaForge.API.Data;

var builder = WebApplication.CreateBuilder(args);


// ================================
// DATABASE
// ================================

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


// ================================
// CONTROLLERS
// ================================

builder.Services.AddControllers();


// ================================
// OPENAPI / SWAGGER
// ================================

builder.Services.AddOpenApi();


var app = builder.Build();


// ================================
// DATABASE SEEDING
// ================================

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    DbSeeder.SeedAsync(db).Wait();
}


// ================================
// HTTP PIPELINE
// ================================

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();


// ================================
// MAP API CONTROLLERS
// ================================

app.MapControllers();


app.Run();
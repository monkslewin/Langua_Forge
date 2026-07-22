using Microsoft.EntityFrameworkCore;
using LanguaForge.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// Swagger endpoint
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.MapControllers();

app.Run();
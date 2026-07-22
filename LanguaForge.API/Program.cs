using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LanguaForge.API.Data;
using LanguaForge.API.Models;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);


builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers();

builder.Services.AddOpenApi();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();


app.MapControllers();


app.Run();
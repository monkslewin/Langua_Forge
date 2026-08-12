using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using LanguaForge.API.Data;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);


var databaseProvider = builder.Configuration["DatabaseProvider"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (databaseProvider == "SqlServer")
    {
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection"));
    }
    else
    {
        options.UseSqlite(
            builder.Configuration.GetConnectionString("DefaultConnection"));
    }
});


// Identity
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


// JWT Authentication
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            JwtBearerDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Check who created the token
            ValidateIssuer = true,

            // Check who the token is intended for
            ValidateAudience = true,

            // Check if token is expired
            ValidateLifetime = true,

            // Check token signature
            ValidateIssuerSigningKey = true,


            ValidIssuer =
                builder.Configuration["Jwt:Issuer"],


            ValidAudience =
                builder.Configuration["Jwt:Audience"],


            IssuerSigningKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(
                        builder.Configuration["Jwt:Key"]!
                    )
                )
        };
    });



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



// Seed database
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await DbSeeder.SeedAsync(db);
}



app.Run();
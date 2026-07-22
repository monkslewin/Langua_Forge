namespace LanguaForge.API.Data;
using Microsoft.EntityFrameworkCore;
using LanguaForge.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }

    public DbSet<Verb> Verbs { get; set; }

    public DbSet<Conjugation> Conjugations { get; set; }
}
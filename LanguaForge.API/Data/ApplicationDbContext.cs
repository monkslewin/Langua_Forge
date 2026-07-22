using Microsoft.EntityFrameworkCore;
using LanguaForge.API.Models;


namespace LanguaForge.API.Data;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options
    ) : base(options)
    {

    }

    public DbSet<Verb> Verbs { get; set; }

    public DbSet<Conjugation> Conjugations { get; set; }

}
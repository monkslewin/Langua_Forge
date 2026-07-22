using Microsoft.EntityFrameworkCore;
using LanguaForge.API.Models;

namespace LanguaForge.API.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Verb> Verbs { get; set; }

    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {

    }
}
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

    public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();

    public DbSet<Prompt> Prompts => Set<Prompt>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Prompt>()
            .Property(p => p.Level)
            .HasConversion<string>();
    }
}
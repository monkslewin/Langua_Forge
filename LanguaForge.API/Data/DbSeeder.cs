using System.Text.Json;
using LanguaForge.API.Models;

namespace LanguaForge.API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        // Make sure database exists
        await context.Database.EnsureCreatedAsync();


        // Stop if database already has verbs
        if (context.Verbs.Any())
        {
            return;
        }


        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        // Read verbs JSON
        var verbsJson = await File.ReadAllTextAsync(
            "DataFiles/verbs.json"
        );


        var verbs = JsonSerializer.Deserialize<List<Verb>>(
            verbsJson,
            options
        );


        if (verbs != null)
        {
            await context.Verbs.AddRangeAsync(verbs);
            await context.SaveChangesAsync();
        }


        // Read conjugations JSON
        var conjugationsJson = await File.ReadAllTextAsync(
            "DataFiles/conjugations.json"
        );


        var conjugations = JsonSerializer.Deserialize<List<Conjugation>>(
            conjugationsJson,
            options
        );


        if (conjugations != null)
        {
            await context.Conjugations.AddRangeAsync(conjugations);
            await context.SaveChangesAsync();
        }
    }
}
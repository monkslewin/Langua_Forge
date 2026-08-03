namespace LanguaForge.API.Models;

public class JournalEntry
{
    public int Id { get; set; }

    public string Prompt { get; set; } = "";

    public string Response { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string UserId { get; set; } = "";

    public ApplicationUser? User { get; set; }
}

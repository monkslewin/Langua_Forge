namespace LanguaForge.API.DTOs.Journal;

public class JournalEntryResponse
{
    public int Id { get; set; }
    public string Prompt { get; set; } = "";
    public string Response { get; set; } = "";
    public DateTime CreatedAt { get; set; }
}

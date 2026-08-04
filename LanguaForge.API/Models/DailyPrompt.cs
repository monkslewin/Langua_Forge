namespace LanguaForge.API.Models;

public class DailyPrompt
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;

    public int PromptId { get; set; }
    public Prompt Prompt { get; set; } = null!;

    public DateOnly Date { get; set; }
}
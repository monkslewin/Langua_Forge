namespace LanguaForge.API.Models;

public class DailyPrompt
{
    public int Id { get; set; }

    public string UserId { get; set; } = "";

    public int PromptId { get; set; }

    public DateOnly Date { get; set; }

    public Prompt Prompt { get; set; } = null!;
}
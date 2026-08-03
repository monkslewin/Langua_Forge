namespace LanguaForge.API.Models;

public enum PromptLevel
{
    Beginner,
    Intermediate,
    Advanced
}

public class Prompt
{
    public int Id { get; set; }

    public PromptLevel Level { get; set; }

    public string Text { get; set; } = "";
}

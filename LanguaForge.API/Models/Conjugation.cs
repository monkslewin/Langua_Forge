namespace LanguaForge.API.Models;

public class Conjugation
{
    public int Id { get; set; }

    public string Tense { get; set; } = "";

    public string Person { get; set; } = "";

    public string Form { get; set; } = "";

    public int VerbId { get; set; }
    
    public Verb Verb { get; set; } = null!;
}
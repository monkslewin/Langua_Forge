namespace LanguaForge.API.Models;

public class Conjugation
{
    public int Id { get; set; }


    // Foreign key
    public int VerbId { get; set; }


    // Navigation property
    public Verb Verb { get; set; } = null!;


    public string Tense { get; set; } = string.Empty;


    public string Mood { get; set; } = string.Empty;


    public string Person { get; set; } = string.Empty;


    public string Number { get; set; } = string.Empty;


    public string Form { get; set; } = string.Empty;
}
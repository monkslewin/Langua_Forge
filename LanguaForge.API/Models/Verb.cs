namespace LanguaForge.API.Models;

public class Verb
{
    public int Id { get; set; }

    public string Infinitive { get; set; } = "";

    public string English { get; set; } = "";

    public string Group { get; set; } = "";

    public bool IsIrregular { get; set; }

    public bool IsReflexive { get; set; }

    public int FrequencyRank { get; set; }

    public List<Conjugation> Conjugations { get; set; } = new();
}
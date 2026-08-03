using System.ComponentModel.DataAnnotations;

namespace LanguaForge.API.DTOs.Journal;

public class CreateJournalEntryRequest
{
    [Required]
    public string Prompt { get; set; } = "";

    [Required]
    public string Response { get; set; } = "";
}
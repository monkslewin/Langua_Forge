using System.ComponentModel.DataAnnotations;

namespace LanguaForge.API.DTOs.Journal;

public class UpdateJournalEntryRequest
{
    [Required]
    public string Prompt { get; set; } = "";

    [Required]
    public string Response { get; set; } = "";
}

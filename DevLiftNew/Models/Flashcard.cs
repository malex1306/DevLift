// Models/Flashcard.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevLiftNew.Models;

public class Flashcard
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Frage ist erforderlich!")]
    [Display(Name = "Frage")]
    public string Front { get; set; }

    [Required(ErrorMessage = "Antwort ist erforderlich!")]
    [Display(Name = "Antwort")]
    public string Back { get; set; }

    [Required]
    public string Category { get; set; } // "Netzwerke", "BWL", "Pseudocode"

    // Verknüpfung zum AppUser
    [ForeignKey("CreatedBy")]
    public string CreatedById { get; set; }
    public AppUser CreatedBy { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime LastReviewed { get; set; } = DateTime.Now;
}
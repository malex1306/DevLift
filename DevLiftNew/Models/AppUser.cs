using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DevLiftNew.Models;

public class AppUser : IdentityUser 
{
    [Required(ErrorMessage = "Vorname ist erforderlich!")]
    [Display(Name = "Vorname")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Nachname ist erforderlich!")]
    [Display(Name = "Nachname")]
    public string? LastName { get; set; }
    
    [DataType(DataType.DateTime)]
    public DateTime? RegistrationDate { get; set; } = DateTime.Now;
    
    [NotMapped]
    [Required(ErrorMessage = "Passwort ist erforderlich")]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "Das {0} Passwort muss mindestens {2} Zeichen lang sein", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", 
        ErrorMessage = "Passwort muss Groß-/Kleinbuchstaben, Zahlen und Sonderzeichen enthalten")]
    public string Password { get; set; }
    
    [NotMapped]
    [DataType(DataType.Password)]
    [Compare("Passwort", ErrorMessage = "Passwörter stimmen nicht überein")]
    public string ConfirmPassword { get; set; }
    
}
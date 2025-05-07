using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevLiftNew.Models
{
    public class Flashcard
    {
        public int Id { get; set; }

        [Required]
        public string Front { get; set; }

        [Required]
        public string Back { get; set; }

        [Required]
        public string Category { get; set; }

        // Foreign Key zu AppUser (Der Benutzer, der die Karteikarte erstellt hat)
        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        
        // Navigationseigenschaft, die den Benutzer referenziert, der die Karteikarte erstellt hat
        public AppUser CreatedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime LastReviewed { get; set; } = DateTime.Now;
    }
}
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

        
        [ForeignKey("CreatedBy")]
        public string CreatedById { get; set; }
        
        
        public AppUser CreatedBy { get; set; }
        
        [DataType(DataType.DateTime)]
        public DateTime LastReviewed { get; set; } = DateTime.Now;
    }
}
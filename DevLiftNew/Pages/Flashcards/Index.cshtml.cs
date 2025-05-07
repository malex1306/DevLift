using DevLiftNew.Data;
using DevLiftNew.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Linq;

namespace DevLiftNew.Pages.Flashcards
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        
        // Property für Kategorien und ihre Karten
        public List<CategoryWithFlashcards> Categories { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        // Laden der Karten, gruppiert nach Kategorien
        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Abfragen der Kategorien und den dazugehörigen Karten des Benutzers
            Categories = await _context.Flashcards
                .Where(f => f.CreatedById == userId)
                .GroupBy(f => f.Category)  // Gruppierung nach Kategorie
                .Select(g => new CategoryWithFlashcards
                {
                    Name = g.Key,  // Der Name der Kategorie (z.B. "Mathematik", "Englisch")
                    Flashcards = g.ToList()  // Die Karten der jeweiligen Kategorie
                })
                .ToListAsync();
        }

        // Handler für das Löschen einer Karte
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Karte anhand der ID und Benutzer-ID suchen
            var card = await _context.Flashcards
                .Where(f => f.Id == id && f.CreatedById == userId)
                .FirstOrDefaultAsync();

            if (card == null)
            {
                return NotFound();
            }

            // Karte aus der Datenbank löschen
            _context.Flashcards.Remove(card);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }

    // Hilfsklasse, die eine Kategorie mit den dazugehörigen Karten kapselt
    public class CategoryWithFlashcards
    {
        public string Name { get; set; }
        public List<Flashcard> Flashcards { get; set; }
    }
}

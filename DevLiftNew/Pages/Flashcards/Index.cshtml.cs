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
        
        
        public List<CategoryWithFlashcards> Categories { get; set; }

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        
        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            Categories = await _context.Flashcards
                .Where(f => f.CreatedById == userId)
                .GroupBy(f => f.Category)  
                .Select(g => new CategoryWithFlashcards
                {
                    Name = g.Key,  
                    Flashcards = g.ToList()  
                })
                .ToListAsync();
        }


        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            var card = await _context.Flashcards
                .Where(f => f.Id == id && f.CreatedById == userId)
                .FirstOrDefaultAsync();

            if (card == null)
            {
                return NotFound();
            }

            
            _context.Flashcards.Remove(card);
            await _context.SaveChangesAsync();

            return RedirectToPage();
        }
    }

    
    public class CategoryWithFlashcards
    {
        public string Name { get; set; }
        public List<Flashcard> Flashcards { get; set; }
    }
}

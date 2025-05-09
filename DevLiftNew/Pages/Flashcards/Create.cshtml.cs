// Pages/Flashcards/Create.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DevLiftNew.Data;  // Für AppDbContext
using DevLiftNew.Models; // Für Flashcard
using System.Security.Claims; // Für User Claims
using Microsoft.EntityFrameworkCore; // Für SaveChangesAsync
using Microsoft.AspNetCore.Identity;  // Für UserManager

namespace DevLiftNew.Pages.Flashcards
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;  

        [BindProperty] public Flashcard Flashcard { get; set; }

        public CreateModel(AppDbContext context, UserManager<AppUser> userManager)  
        {
            _context = context;
            _userManager = userManager;  
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine($"Angemeldet? {User.Identity?.IsAuthenticated}");
    
            
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"Claim: {claim.Type} = {claim.Value}");
            }

            Console.WriteLine("OnPostAsync wurde aufgerufen");

           
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                ModelState.AddModelError(string.Empty, "Du bist nicht eingeloggt.");
                Console.WriteLine("Fehler: Benutzer ist nicht eingeloggt.");
                return Page();
            }

            Console.WriteLine($"Ermittelter Benutzer-ID aus den Claims: {userId}");

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Fehler: Benutzer konnte nicht gefunden werden.");
                return Page();
            }

            Flashcard.CreatedById = userId;
            Flashcard.LastReviewed = DateTime.Now;

            _context.Flashcards.Add(Flashcard);
            await _context.SaveChangesAsync();

            Console.WriteLine("Flashcard erfolgreich gespeichert.");
            return RedirectToPage("./Index");
        }
        
    }
    
}


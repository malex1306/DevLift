using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using DevLiftNew.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DevLiftNew.Pages
{
    public class QuizNetwerke : PageModel
    {
        private readonly ILogger<QuizNetwerke> _logger;
        private readonly UserManager<AppUser> _userManager;
        
        public int CompletionRate { get; set; } = 75;
        public string WelcomeMessage { get; private set; }

        public QuizNetwerke(
            ILogger<QuizNetwerke> logger,
            UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task OnGetAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                {
                   
                    WelcomeMessage = $"Hey, {user.FirstName ?? "Freund"}!";
                }
                else
                {
                    // Wenn der Benutzer eingeloggt ist, aber kein FirstName gefunden wurde
                    WelcomeMessage = "Willkommen zurück!";
                }
            }
            else
            {
                // Wenn der Benutzer nicht eingeloggt ist
                WelcomeMessage = "Willkommen bei DevLift!";
            }
        }
    }
}
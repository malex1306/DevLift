using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using DevLiftNew.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace DevLiftNew.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        
        public int CompletionRate { get; set; } = 75;
        public string WelcomeMessage { get; private set; }

        public IndexModel(
            ILogger<IndexModel> logger,
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
                    // Wenn der Benutzer eingeloggt ist und einen FirstName hat
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
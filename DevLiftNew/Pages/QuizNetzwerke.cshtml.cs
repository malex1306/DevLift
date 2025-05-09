using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using DevLiftNew.Models;
using DevLiftNew.Data;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace DevLiftNew.Pages
{
    public class QuizNetwerke : PageModel
    {
        private readonly ILogger<QuizNetwerke> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public int CompletionRate { get; set; } = 75;
        public string WelcomeMessage { get; private set; } = "Willkommen bei DevLift!";
        public List<QuizQuestion> Fragen { get; private set; } = new();

        public QuizNetwerke(ILogger<QuizNetwerke> logger, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task OnGetAsync()
        {
            // Benutzername setzen, falls angemeldet
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                WelcomeMessage = user != null ? $"Hey, {user.FirstName ?? "Freund"}!" : "Willkommen zurück!";
            }

            // Fragen aus DB laden und mit Antworten verknüpfen
            var fragenAusDb = _dbContext.QuizQuestions
                .Where(q => q.Kategorie == "Netzwerke")
                .Select(q => new QuizQuestion
                {
                    Id = q.Id,
                    FrageText = q.FrageText,
                    Kategorie = q.Kategorie,
                    Answers = q.Answers.ToList()
                }).ToList();

            // Fragen zufällig mischen
            var rnd = new Random();
            Fragen = fragenAusDb.OrderBy(_ => rnd.Next()).ToList();

            // Antworten innerhalb jeder Frage zufällig mischen
            foreach (var frage in Fragen)
            {
                frage.Answers = frage.Answers.OrderBy(_ => rnd.Next()).ToList();
            }
        }

        public async Task<IActionResult> OnPostAddQuestionAsync(string frageText, string antworten, string kategorie)
        {
            if (string.IsNullOrWhiteSpace(frageText) || string.IsNullOrWhiteSpace(antworten) || string.IsNullOrWhiteSpace(kategorie))
                return RedirectToPage();

            var antwortList = antworten.Split(',')
                                       .Select((text, index) => new QuizAnswer
                                       {
                                           AntwortText = text.Trim(),
                                           IstKorrekt = index == 0 // Erste Antwort gilt als korrekt
                                       })
                                       .ToList();

            var neueFrage = new QuizQuestion
            {
                FrageText = frageText.Trim(),
                Kategorie = kategorie.Trim(),
                Answers = antwortList
            };

            _dbContext.QuizQuestions.Add(neueFrage);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}

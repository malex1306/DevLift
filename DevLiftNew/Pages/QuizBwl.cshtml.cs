using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using DevLiftNew.Models;
using DevLiftNew.Data;
using Microsoft.EntityFrameworkCore;

namespace DevLiftNew.Pages
{
    public class QuizBwlModel : PageModel
    {
        private readonly ILogger<QuizBwlModel> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _dbContext;

        public int CompletionRate { get; set; } = 75;
        public string WelcomeMessage { get; set; } = "Willkommen bei DevLift";
        public List<QuizQuestionBwl> FragenBwl { get; private set; } = new();

        public QuizBwlModel(ILogger<QuizBwlModel> logger, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [BindProperty]
        public List<QuizAntwortModel>Antworten { get; set; } = new();

        public async Task OnGetAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(User);
                WelcomeMessage = user != null ? $"Hey, {user.FirstName ?? "Freund"}!" : "Willkommen zurück!";
            }

            var fragenAusDb = _dbContext.QuizQuestionsBwl
                .Where(a => a.BwlKategorie == "Bwl")
                .Include(a => a.BwlAnswers)
                .Select(a => new QuizQuestionBwl
                {
                    Id = a.Id,
                    BwlFrageText = a.BwlFrageText,
                    BwlKategorie = a.BwlKategorie,
                    BwlAnswers = a.BwlAnswers!.ToList()
                }).ToList();

            var rnd = new Random();
            FragenBwl = fragenAusDb.OrderBy(_ => rnd.Next()).ToList();

            foreach (var frage in FragenBwl)
            {
                if (frage.BwlAnswers != null)
                    frage.BwlAnswers = frage.BwlAnswers.OrderBy(_ => rnd.Next()).ToList();
            }
        }

        public async Task<IActionResult> OnPostAsync(int Punkte, int MaxPunkte, int Prozent, string AntwortenJson)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            List<QuizAntwortModel>? antworten;
            try
            {
                antworten = JsonSerializer.Deserialize<List<QuizAntwortModel>>(AntwortenJson);
            }
            catch
            {
                return BadRequest("Fehler beim Verarbeiten der Antworten.");
            }

            if (antworten == null || antworten.Count == 0)
                return BadRequest("Keine Antworten empfangen.");

            var result = new QuizResultBwl
            {
                Punkte = Punkte,
                MaxPunkte = MaxPunkte,
                Prozent = Prozent,
                UserId = user.Id,
                Date = DateTime.Now
            };

            _dbContext.QuizResultBwl.Add(result);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/ResultPage", new { quizId = result.Id });
        }
        

        public async Task<IActionResult> OnPostAddQuestionAsync(string bwlFrageText, string bwlAntworten, string bwlKategorie)
        {
            if (string.IsNullOrWhiteSpace(bwlFrageText) || string.IsNullOrWhiteSpace(bwlAntworten) || string.IsNullOrWhiteSpace(bwlKategorie))
                return RedirectToPage();

            var bwlAntwotenList = bwlAntworten
                .Split(",")
                .Select(text => new QuizAnswerBwl
                {
                    BwlAntwortText = text.Trim(),
                    BwlIstKorrekt = text.Trim().Contains("korrekt", StringComparison.CurrentCultureIgnoreCase)
                })
                .ToList();

            var neueFrage = new QuizQuestionBwl
            {
                BwlFrageText = bwlFrageText.Trim(),
                BwlKategorie = bwlKategorie.Trim(),
                BwlAnswers = bwlAntwotenList
            };

            _dbContext.QuizQuestionsBwl.Add(neueFrage);
            await _dbContext.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}

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
        public string WelcomeMessage { get; set; } = "Willkomen bei DevLift";
        public List<QuizQuestionBwl> FragenBwl { get; private set; } = new();

        private int correctCount = 0;
        private int wrongCount = 0;
        private HashSet<int> answeredQuestions = new HashSet<int>();
        private Dictionary<int, int> userAnswers = new Dictionary<int, int>(); // FragenId -> AntwortId

        public QuizBwlModel(ILogger<QuizBwlModel> logger, UserManager<AppUser> userManager, AppDbContext dbContext)
        {
            _logger = logger;
            _userManager = userManager;
            _dbContext = dbContext;
        }

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
                    BwlAnswers = a.BwlAnswers.ToList()
                }).ToList();

            var rnd = new Random();
            FragenBwl = fragenAusDb.OrderBy(_ => rnd.Next()).ToList();

            foreach (var fragen in FragenBwl)
            {
                fragen.BwlAnswers = fragen.BwlAnswers.OrderBy(_ => rnd.Next()).ToList();
            }
        }

        public async Task<IActionResult> OnPostSaveResultAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }

            var result = new QuizResultBwl
            {
                UserId = user.Id,
                Date = DateTime.Now,
                Punkte = correctCount,
                MaxPunkte = correctCount + wrongCount,
                Prozent = (int)((double)correctCount / (correctCount + wrongCount) * 100),
                BeantworteteFragen = new List<QuizResultQuestion>()
            };

            foreach (var fragenId in answeredQuestions)
            {
                if (FragenBwl.FirstOrDefault(f => f.Id == fragenId) is { } frage && userAnswers.TryGetValue(fragenId, out var antwortId))
                {
                    var richtigeAntwortVorhanden = frage.BwlAnswers.Any(a => a.Id == antwortId && a.BwlIstKorrekt);
                    result.BeantworteteFragen.Add(new QuizResultQuestion
                    {
                        QuizQuestionBwlId = fragenId,
                        IsCorrect = richtigeAntwortVorhanden
                    });
                }
            }

            try
            {
                _dbContext.QuizResultBwl.Add(result);
                await _dbContext.SaveChangesAsync();
                return RedirectToPage("/ResultPage", new { userId = user.Id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Fehler beim Speichern der Quiz-Ergebnisse: {ex.Message}");
                return RedirectToPage("/Error");
            }
        }

        public void CheckAnswer(int fragenId, int antwortId)
        {
            
            if (answeredQuestions.Contains(fragenId)) return;

            answeredQuestions.Add(fragenId);
            userAnswers[fragenId] = antwortId; // Speichere die Antwort des Benutzers

            var frage = FragenBwl.First(f => f.Id == fragenId);
            var antwort = frage.BwlAnswers.First(a => a.Id == antwortId);

            if (antwort.BwlIstKorrekt)
            {
                correctCount++;
            }
            else
            {
                wrongCount++;
            }

            if (answeredQuestions.Count == FragenBwl.Count)
            {
                OnPostSaveResultAsync().Wait();
            }
        }

        public async Task<IActionResult> OnPostAddQuestionAsync(string bwlFrageText, string bwlAntworten,
            string bwlKategorie)
        {
            if (string.IsNullOrWhiteSpace(bwlFrageText) || string.IsNullOrWhiteSpace(bwlAntworten) ||
                string.IsNullOrWhiteSpace(bwlKategorie))
                return RedirectToPage();
            var bwlAntwotenList = bwlAntworten
                .Split(",")
                .Select((text, index) => new QuizAnswerBwl()
                {
                    BwlAntwortText = text.Trim(),
                    BwlIstKorrekt = text.Trim().ToLower().Contains("korrekt") // Beispielhafte Logik, anpassen!
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
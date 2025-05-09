using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DevLiftNew.Data;
using DevLiftNew.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevLiftNew.Pages
{
    public class ImportModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImportModel(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public string ImportMessage { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            var path = Path.Combine(_env.WebRootPath, "data", "quizfragen.json");
            if (!System.IO.File.Exists(path))
            {
                ImportMessage = "Datei nicht gefunden.";
                return Page();
            }

            string json;
            try
            {
                json = await System.IO.File.ReadAllTextAsync(path);
            }
            catch (IOException)
            {
                ImportMessage = "Fehler beim Lesen der Datei.";
                return Page();
            }

            List<QuizQuestion>? fragen;
            try
            {
                // Case-insensitive Deserialisierung
                fragen = JsonSerializer.Deserialize<List<QuizQuestion>>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (fragen == null || !fragen.Any())
                {
                    ImportMessage = "Die JSON-Datei enthält keine gültigen Fragen.";
                    return Page();
                }
            }
            catch (JsonException)
            {
                ImportMessage = "Fehler beim Deserialisieren der JSON-Datei.";
                return Page();
            }

            foreach (var frage in fragen)
            {
                if (!_context.QuizQuestions.Any(f => f.FrageText == frage.FrageText))
                {
                    _context.QuizQuestions.Add(frage);
                }
            }

            await _context.SaveChangesAsync();

            ImportMessage = "Import erfolgreich!";
            return Page();
        }
    }
}

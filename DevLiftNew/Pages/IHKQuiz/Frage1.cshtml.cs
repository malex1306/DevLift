using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using DevLiftNew.Models;
using Microsoft.AspNetCore.Identity;

namespace DevLiftNew.Pages.IHKQuiz
{
    public class Frage1Model : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        [BindProperty]
        public string UserCode { get; set; }
        public string Feedback { get; set; }
        public bool IsCorrect { get; set; }
        public string WelcomeMessage { get; set; }

        public Frage1Model(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            WelcomeMessage = user != null ? $"Hey, {user.FirstName ?? "Freund"}!" : "Willkommen zurück!";
            
            if (string.IsNullOrWhiteSpace(UserCode))
            {
                Feedback = "Bitte gib deinen Code ein.";
                return Page();
            }

            try
            {
                var options = ScriptOptions.Default
                    .WithImports("System")
                    .WithReferences(typeof(object).Assembly);

               
                int zahl = 4;

               
                var fullCode = $@"
                    int zahl = {zahl};
                    {UserCode}
                ";

                var result = await CSharpScript.EvaluateAsync<object>(fullCode, options);

                if (result is bool b && b)
                {
                    Feedback = "Richtig – die Zahl ist gerade!";
                    IsCorrect = true;
                }
                else
                {
                    Feedback = $"Falsch oder kein boolescher Ausdruck. Ergebnis war: {result}";
                }
            }
            catch (CompilationErrorException e)
            {
                Feedback = $"Kompilierungsfehler:\n{string.Join("\n", e.Diagnostics)}";
            }
            catch (Exception ex)
            {
                Feedback = $"Laufzeitfehler: {ex.Message}";
            }

            return Page();
        }
    }
}
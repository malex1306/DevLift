using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace DevLiftNew.Pages.IHKQuiz;

public class Frage6Model : PageModel
{
    [BindProperty]
    
        public string UserCode { get; set; }
        public string Feedback { get; set; }
        public bool IsCorrect { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
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

                var testCode = $@"{UserCode}
                    bool t1 = IstSpiegelwort(""""abccba"""");
                    bool t2 = IstSpiegelwort(""""abcdba"""");
                    bool t3 = IstSpiegelwort(""""123321"""");

                    t1 && !t2 && t3";
                
                var result = await CSharpScript.EvaluateAsync<bool>(testCode, options);

                if (result)
                {
                    Feedback = "Richtig - deine Methode erkennt Spiegelwörter korrekt!";
                    IsCorrect = true;
                }
                else
                {
                    Feedback = "Leider falsch. prüfe, ob deine Methode alle Tests korrekt erkennt";
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevLiftNew.Pages.IHKQuiz;

public class Frage5Model : PageModel
{
    [BindProperty]
    public string SelectedAnswer { get; set; }
    public string Feedback { get; set; }
    public bool IsCorrect { get; set; }
    
    public void OnPost()
    {
        if (string.IsNullOrWhiteSpace(SelectedAnswer))
        {
            Feedback = "Bitte wähle eine Antwort aus.";
            IsCorrect = false;
            return;
        }

        if (SelectedAnswer == "A")
        {
            Feedback = "Richtig - Die Aussage 3 ist korrekt!";
            IsCorrect = true;
        }
        else
        {
            Feedback = "Leider falsch. Überprüfe deine Antwort.";
            IsCorrect = false;  
        }
    }
}
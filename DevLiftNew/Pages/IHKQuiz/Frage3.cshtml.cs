using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevLiftNew.Pages.IHKQuiz;

public class Frage3Model : PageModel
{
    [BindProperty]
    public string SelectedAnswer { get; set; }
    public string Feedback { get; set; }
    public bool IsCorrect { get; set; }

    public void OnPost()
    {
        if (string.IsNullOrEmpty(SelectedAnswer))
        {
            Feedback = "Bitte wähle eine Antwort aus.";
            IsCorrect = false;
            return;
        }

        if (SelectedAnswer == "B")
        {
            Feedback = "Richtig – durch das explizite Casting von x zu double wird auch y zu double konvertiert, Ergebnis: 3.0";
            IsCorrect = true;
        }
        else
        {
            Feedback = "Leider falsch. Denk an die Typumwandlung – einer der Operanden ist ein double.";
            IsCorrect = false;
        }
    }
}
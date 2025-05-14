using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevLiftNew.Pages.IHKQuiz;

public class Frage2Model : PageModel
{
    [BindProperty]
    public List<string> SelectedAnswers { get; set; } = new List<string>();
    public string Feedback { get; set; }
    public bool IsCorrect { get; set; }

    public void OnPost()
    {
        if (SelectedAnswers == null || SelectedAnswers.Count == 0)
        {
            Feedback = null;
            IsCorrect = false;
            return;
        }
        var correctAnswers = new List<string> { "1", "3" };

        if (SelectedAnswers.OrderBy(x => x).SequenceEqual(correctAnswers.OrderBy(x => x)))
        {
            Feedback = "Richtig - die Aussagen 1 und 3 sind korrekt!";
            IsCorrect = true;
        }
        else
        {
            Feedback = "Leider falsch. Bitte überprüfe deine Antworten.";
            IsCorrect = false;
        }
    }
}
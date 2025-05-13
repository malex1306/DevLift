using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevLiftNew.Pages.IHKQuiz
{
    public class Frage1Model : PageModel
    {
        [BindProperty]
        public string UserCode { get; set; }

        public string Feedback { get; set; }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(UserCode))
            {
                Feedback = "Bitte gib deinen Pseudocode ein.";
                return;
            }

            if (UserCode.ToLower().Contains("if") && UserCode.Contains("%") && UserCode.Contains("== 0"))
            {
                Feedback = "Gute Lösung!";
            }
            else
            {
                Feedback = "Falsch!";
            }
        }
    }

}
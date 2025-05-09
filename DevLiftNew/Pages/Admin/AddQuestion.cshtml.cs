using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DevLiftNew.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevLiftNew.Data;

namespace DevLiftNew.Pages.Admin
{
    public class AddQuestionModel : PageModel
    {
        private readonly AppDbContext _context;

        [BindProperty]
        public QuizQuestion Frage { get; set; }

        [BindProperty]
        public List<QuizAnswer> Antworten { get; set; } = new()
        {
            new QuizAnswer(),
            new QuizAnswer(),
            new QuizAnswer(),
            new QuizAnswer()
        };

        public AddQuestionModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Frage.Answers = Antworten;
            _context.QuizQuestions.Add(Frage);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
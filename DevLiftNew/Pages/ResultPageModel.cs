using Microsoft.AspNetCore.Mvc.RazorPages;
using DevLiftNew.Data;
using System.Linq;
using DevLiftNew.Models;
using Microsoft.EntityFrameworkCore;

namespace DevLiftNew.Pages
{
    public class ResultPageModel : PageModel
    {
        private readonly AppDbContext _context;

        public ResultPageModel(AppDbContext context)
        {
            _context = context;
        }

        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public int Percentage { get; set; }

        public List<QuizResultQuestion> BeantworteteFragen { get; set; } = new();

        public void OnGet(int quizId)
        {
            var result = _context.QuizResultBwl
                .Include(r => r.BeantworteteFragen)
                .ThenInclude(f => f.QuizQuestionBwl)
                .FirstOrDefault(r => r.Id == quizId);

            if (result != null)
            {
                CorrectCount = result.Punkte;
                TotalQuestions = result.MaxPunkte;
                Percentage = result.MaxPunkte > 0 ? (int)((double)CorrectCount / TotalQuestions * 100) : 0;

                BeantworteteFragen = result.BeantworteteFragen;
            }
        }
    }
}
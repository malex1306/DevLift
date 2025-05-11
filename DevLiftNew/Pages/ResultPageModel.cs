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

        public int CorrectCount { get; set; }
        public int TotalQuestions { get; set; }
        public int Percentage { get; set; }
        public List<QuizResultQuestion> BeantworteteFragen { get; set; }

        public ResultPageModel(AppDbContext context)
        {
            _context = context;
        }

        public void OnGet(string userId)
        {
            var results = _context.QuizResultBwl
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Date)
                .FirstOrDefault();

            if (results != null)
            {
                CorrectCount = results.Punkte;
                TotalQuestions = results.MaxPunkte;
                Percentage = results.Prozent;
                
                BeantworteteFragen = _context.QuizResultQuestion
                    .Where(q => q.QuizResultBwlId == results.Id)
                    .Include(q => q.QuizQuestionBwl)  // Frage mit einbeziehen
                    .ToList();
            }
            else
            {
                BeantworteteFragen = new List<QuizResultQuestion>(); 
            }
        }

        
    }
}
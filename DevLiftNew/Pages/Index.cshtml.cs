using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using DevLiftNew.Models;
using System.Threading.Tasks;
using DevLiftNew.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DevLiftNew.Pages
{
   public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _dbContext;  

    public int CompletionRate { get; set; } = 75;
    public string WelcomeMessage { get; private set; }

    public IndexModel(
        ILogger<IndexModel> logger,
        UserManager<AppUser> userManager,
        AppDbContext dbContext) 
    {
        _logger = logger;
        _userManager = userManager;
        _dbContext = dbContext;  
    }

    public async Task OnGetAsync()
    {
        AppUser user = null; 

        if (User.Identity?.IsAuthenticated == true)
        {
            user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                WelcomeMessage = $"Hey, {user.FirstName ?? "Freund"}!";
            }
            else
            {
                WelcomeMessage = "Willkommen zurück!";
            }

            
            var progress = await GetUserProgressAsync(user.Id);
            ViewData["UserProgress"] = progress;  
        }
        else
        {
            WelcomeMessage = "Willkommen bei DevLift!";
        }
    }

   
    public async Task<double> GetUserProgressAsync(string userId)
    {
       
        var totalQuizzes = await _dbContext.QuizQuestionsBwl.CountAsync();  // Gesamtzahl der Quizzes
        var completedQuizzes = await _dbContext.QuizResultBwl
            .Where(r => r.UserId == userId)
            .CountAsync();  

        return (totalQuizzes > 0) ? (double)completedQuizzes / totalQuizzes * 100 : 0;
    }
}

}
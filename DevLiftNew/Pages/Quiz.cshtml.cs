using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DevLiftNew.Pages;

public class Quiz : PageModel
{
    private readonly ILogger<Quiz> _logger;

    public Quiz(ILogger<Quiz> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
        
    }
}
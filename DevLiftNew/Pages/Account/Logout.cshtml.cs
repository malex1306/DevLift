using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using DevLiftNew.Models;

namespace DevLiftNew.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;

        public LogoutModel(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task OnGetAsync()
        {
            await _signInManager.SignOutAsync();
            // Optional: Clear all session/cookie data if needed
            await HttpContext.SignOutAsync();
        }
    }
}
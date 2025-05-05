using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using DevLiftNew.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;

public class LoginModel : PageModel
{
    private readonly SignInManager<AppUser> _signInManager;
    private readonly UserManager<AppUser> _userManager;

    public LoginModel(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var user = await _userManager.FindByEmailAsync(Input.Email);
        if (user != null)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, Input.Password, false);
            if (result.Succeeded)
            {
                
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

               
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName ?? ""),
                    new Claim("given_name", user.FirstName ?? ""),
                    new Claim("family_name", user.LastName ?? "")
                };

                var identity = new ClaimsIdentity(claims, IdentityConstants.ApplicationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme, principal);

                return RedirectToPage("/Index");
            }
        }

        ModelState.AddModelError(string.Empty, "Ungültige Anmeldedaten");
        return Page();
    }
}

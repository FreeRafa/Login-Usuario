using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Login_User.Pages.Dashboard
{
    public class DashboardModel : PageModel
    {
        public string NomeUsuario { get; private set; } = "Visitante";

        public IActionResult OnGet()
        {
            var nome = HttpContext.Session.GetString("UsuarioNome");

            if (string.IsNullOrEmpty(nome))
                return RedirectToPage("/Auth/Login");

            NomeUsuario = nome;
            return Page();
        }
    }
}
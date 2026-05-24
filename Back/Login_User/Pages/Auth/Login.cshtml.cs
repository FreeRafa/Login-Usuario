using Login_User.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Login_User.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "O e-mail é obrigatório")]
            [EmailAddress(ErrorMessage = "E-mail inválido")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória")]
            [DataType(DataType.Password)]
            public string Senha { get; set; } = string.Empty;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // TODO: chamar RepositorioUser para validar credenciais
            // Exemplo:
            // var user = _repositorio.BuscarPorEmail(Input.Email);
            // if (user == null || !BCrypt.Verify(Input.Senha, user.SenhaHash))
            // {
            //     ModelState.AddModelError(string.Empty, "E-mail ou senha incorretos.");
            //     return Page();
            // }
            HttpContext.Session.SetString("UsuarioNome", Input.Nome);

            return RedirectToPage("/Dashboard/Index");
        }
    }
}

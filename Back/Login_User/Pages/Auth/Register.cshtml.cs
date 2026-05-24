using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Login_User.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public InputModel Input { get; set; } = new();

        public class InputModel
        {
            [Required(ErrorMessage = "O nome é obrigatório")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Nome deve ter entre 2 e 100 caracteres")]
            public string Nome { get; set; } = string.Empty;

            [Required(ErrorMessage = "O e-mail é obrigatório")]
            [EmailAddress(ErrorMessage = "E-mail inválido")]
            public string Email { get; set; } = string.Empty;

            [Required(ErrorMessage = "A senha é obrigatória")]
            [StringLength(255, MinimumLength = 8, ErrorMessage = "A senha deve ter no mínimo 8 caracteres")]
            [DataType(DataType.Password)]
            public string Senha { get; set; } = string.Empty;

            [Required(ErrorMessage = "Confirme a senha")]
            [DataType(DataType.Password)]
            [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
            public string ConfirmarSenha { get; set; } = string.Empty;
        }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // TODO: verificar se email já existe e salvar com hash
            // var hash = BCrypt.HashPassword(Input.Senha);
            // var user = new User { Nome = Input.Nome, Email = Input.Email, SenhaHash = hash };
            // _repositorio.Inserir(user);

            return RedirectToPage("/Auth/Login");
        }
    }
}
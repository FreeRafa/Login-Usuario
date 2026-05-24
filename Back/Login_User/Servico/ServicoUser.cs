using Login_User.Model;
using Login_User.Repositorio;
using System.Security.Cryptography;
using System.Text;

namespace Login_User.Service
{
    public class UserService
    {
        private readonly RepositorioUser _repositorio;

        public UserService(RepositorioUser repositorio)
        {
            _repositorio = repositorio;
        }

        // -------------------------------------------------------
        // CADASTRAR — Valida e regista um novo utilizador
        // -------------------------------------------------------
        public (bool sucesso, string mensagem) Cadastrar(string nome, string email, string senha)
        {
            // Validações básicas
            if (string.IsNullOrWhiteSpace(nome))
                return (false, "O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
                return (false, "Email inválido.");

            if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
                return (false, "A senha deve ter pelo menos 6 caracteres.");

            // Verifica se o email já está em uso
            if (_repositorio.EmailExiste(email))
                return (false, "Este email já está registado.");

            var user = new User
            {
                Nome = nome.Trim(),
                Email = email.Trim().ToLower(),
                SenhaHash = GerarHash(senha),
                Ativo = true,
                DataCriacao = DateTime.UtcNow
            };

            _repositorio.Registar(user);

            return (true, "Cadastro realizado com sucesso.");
        }

        // -------------------------------------------------------
        // LOGIN — Valida email e senha, devolve o utilizador
        // -------------------------------------------------------
        public (bool sucesso, string mensagem, User? user) Login(string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
                return (false, "Email e senha são obrigatórios.", null);

            var user = _repositorio.BuscarPorEmail(email.Trim().ToLower());

            if (user == null)
                return (false, "Email ou senha incorretos.", null);

            if (!user.Ativo)
                return (false, "Esta conta está inativa.", null);

            if (!VerificarHash(senha, user.SenhaHash!))
                return (false, "Email ou senha incorretos.", null);

            return (true, "Login efetuado com sucesso.", user);
        }

        // -------------------------------------------------------
        // ALTERAR SENHA — Valida a senha atual e aplica a nova
        // -------------------------------------------------------
        public (bool sucesso, string mensagem) AlterarSenha(int userId, string senhaAtual, string novaSenha)
        {
            if (string.IsNullOrWhiteSpace(novaSenha) || novaSenha.Length < 6)
                return (false, "A nova senha deve ter pelo menos 6 caracteres.");

            var user = _repositorio.BuscarPorId(userId);

            if (user == null)
                return (false, "Utilizador não encontrado.");

            if (!VerificarHash(senhaAtual, user.SenhaHash!))
                return (false, "A senha atual está incorreta.");

            user.SenhaHash = GerarHash(novaSenha);
            user.DataAlteracao = DateTime.UtcNow;

            _repositorio.Atualizar(user);

            return (true, "Senha alterada com sucesso.");
        }

        // -------------------------------------------------------
        // HASH — SHA256 com salt fixo por email (simples e sem pacotes)
        // -------------------------------------------------------
        private static string GerarHash(string senha)
        {
            // Salt estático simples para projeto académico.
            // Em produção usa BCrypt ou PBKDF2 com salt aleatório por utilizador.
            var conteudo = $"login_user_salt|{senha}";
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(conteudo));
            return Convert.ToHexString(bytes).ToLower();
        }

        private static bool VerificarHash(string senha, string hashGuardado)
        {
            var hashGerado = GerarHash(senha);
            return string.Equals(hashGerado, hashGuardado, StringComparison.OrdinalIgnoreCase);
        }
    }
}
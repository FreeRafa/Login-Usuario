using Login_User.Data;
using Login_User.Model;
using Microsoft.Data.SqlClient;

namespace Login_User.Repositorio
{
    public class RepositorioUser
    {
        // -------------------------------------------------------
        // REGISTAR — Insere um novo utilizador na base de dados
        // -------------------------------------------------------
        public void Registar(User user)
        {
            const string sql = @"
                INSERT INTO Usuario (Nome, Email, SenhaHash, Ativo, DataCriacao)
                VALUES (@Nome, @Email, @SenhaHash, @Ativo, @DataCriacao)";

            using var connection = DbConnectionFactory.GetConnection();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Nome", user.Nome);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@SenhaHash", user.SenhaHash);
            command.Parameters.AddWithValue("@Ativo", user.Ativo);
            command.Parameters.AddWithValue("@DataCriacao", user.DataCriacao ?? DateTime.UtcNow);

            command.ExecuteNonQuery();
        }

        // -------------------------------------------------------
        // BUSCAR POR EMAIL — Usado no login para validar o utilizador
        // -------------------------------------------------------
        public User? BuscarPorEmail(string email)
        {
            const string sql = @"
                SELECT Id, Nome, Email, SenhaHash, Ativo, DataCriacao, DataAlteracao
                FROM Usuario
                WHERE Email = @Email";

            using var connection = DbConnectionFactory.GetConnection();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Email", email);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return MapearUser(reader);
        }

        // -------------------------------------------------------
        // BUSCAR POR ID — Útil para páginas de perfil ou sessão
        // -------------------------------------------------------
        public User? BuscarPorId(int id)
        {
            const string sql = @"
                SELECT Id, Nome, Email, SenhaHash, Ativo, DataCriacao, DataAlteracao
                FROM Usuario
                WHERE Id = @Id";

            using var connection = DbConnectionFactory.GetConnection();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            return MapearUser(reader);
        }

        // -------------------------------------------------------
        // VERIFICAR SE EMAIL JÁ EXISTE — Evita duplicados no cadastro
        // -------------------------------------------------------
        public bool EmailExiste(string email)
        {
            const string sql = "SELECT COUNT(1) FROM Usuario WHERE Email = @Email";

            using var connection = DbConnectionFactory.GetConnection();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Email", email);

            var resultado = (int)command.ExecuteScalar()!;
            return resultado > 0;
        }

        // -------------------------------------------------------
        // ATUALIZAR — Atualiza nome e/ou senha do utilizador
        // -------------------------------------------------------
        public void Atualizar(User user)
        {
            const string sql = @"
                UPDATE Usuario
                SET Nome           = @Nome,
                    SenhaHash      = @SenhaHash,
                    DataAlteracao  = @DataAlteracao
                WHERE Id = @Id";

            using var connection = DbConnectionFactory.GetConnection();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Nome", user.Nome);
            command.Parameters.AddWithValue("@SenhaHash", user.SenhaHash);
            command.Parameters.AddWithValue("@DataAlteracao", DateTime.UtcNow);
            command.Parameters.AddWithValue("@Id", user.Id);

            command.ExecuteNonQuery();
        }

        // -------------------------------------------------------
        // MAPEAMENTO — Converte uma linha do SqlDataReader em User
        // -------------------------------------------------------
        private static User MapearUser(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                Email = reader.GetString(reader.GetOrdinal("Email")),
                SenhaHash = reader.GetString(reader.GetOrdinal("SenhaHash")),
                Ativo = reader.GetBoolean(reader.GetOrdinal("Ativo")),
                DataCriacao = reader.GetDateTime(reader.GetOrdinal("DataCriacao")),
                DataAlteracao = reader.IsDBNull(reader.GetOrdinal("DataAlteracao"))
                                    ? null
                                    : reader.GetDateTime(reader.GetOrdinal("DataAlteracao"))
            };
        }
    }
}
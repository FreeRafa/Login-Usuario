namespace Login_User.Model
{
    public class User
    {
        public int Id { get; set; }
        public string? Nome { get; set; } = string .Empty;
        public string? Email { get; set; } = string.Empty; 
        public string? SenhaHash { get; set; } = string.Empty; // Armazena o hash da senha, não a senha em texto puro
        public bool Ativo { get; set; } = true;
        public DateTime? DataCriacao { get; set; } = DateTime.UtcNow; //Retorna a data e hora atual em UTC
        public DateTime? DataAlteracao { get; set; }
    }

    // Date.Time.UtcNow: Retorna a data e hora atual em UTC (Tempo Universal Coordenado).
    // Isso é útil para garantir que as datas sejam armazenadas de forma consistente, independentemente do fuso horário do servidor ou do usuário.
}

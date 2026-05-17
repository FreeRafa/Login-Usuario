using Microsoft.Data.SqlClient;

namespace Login_User.Data
{
    public class DbConnectionFactory
    {
        private static readonly string _connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=Login_User;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }
    }
}
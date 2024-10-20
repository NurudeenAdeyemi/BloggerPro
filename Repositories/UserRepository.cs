using BloggerPro.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace BloggerPro.Repositories
{
    public class UserRepository
    {
        private static readonly string connectionString = "Server = localhost; User ID = root; Database = BloggerDB; Password=loveforall1990#;";

        public static User GetById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<User>("SELECT * FROM Users WHERE Id = @Id", new { Id = id });
            }
        }

        public static User Login(string username, string password)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<User>(
                    "SELECT * FROM Users WHERE Username = @Username AND Password = @Password",
                    new { Username = username, Password = password });
            }
        }

        public static void Register(User user)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Users (Username, Email, Password) VALUES (@Username, @Email, @Password)", user);
            }
        }
    }
}

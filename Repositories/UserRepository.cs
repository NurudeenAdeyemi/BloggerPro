using BloggerPro.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace BloggerPro.Repositories
{
    public class UserRepository
    {
        private readonly string connectionString = "";

        //register
        public void Register(User user)
        {
            using(var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute($"insert into Users (Username, Email, Password) values ({user.Username}, {user.Email}, {user.Password})");
            }

        }

        //login
        public User Login(string username, string password)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QuerySingleOrDefault<User>($"select from Users where Username = {username}and Password = {password}");
                return user;
            }
        }

        //get a user
        public User GetUser(int userId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                var user = connection.QuerySingleOrDefault<User>($"select from Users where Id = {userId}");
                return user;
            }
        }
    }
}

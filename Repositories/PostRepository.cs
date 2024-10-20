using BloggerPro.Models;
using Dapper;
using MySql.Data.MySqlClient;

namespace BloggerPro.Repositories
{
    public class PostRepository
    {
        private static readonly string connectionString = "Server = localhost; User ID = root; Database = BloggerDB; Password=loveforall1990#";

        public static IEnumerable<Post> GetPostsByCommunity(int communityId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Post>("SELECT * FROM Posts WHERE CommunityId = @CommunityId", new { CommunityId = communityId });
            }
        }

        public static void AddPost(Post post)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Posts (Title, Content, CreatedAt, CommunityId, UserId) VALUES (@Title, @Content, @CreatedAt, @CommunityId, @UserId)", post);
            }
        }
    }
}

using BloggerPro.Models;
using Dapper;
using Microsoft.AspNetCore.Hosting.Server;
using MySql.Data.MySqlClient;

namespace BloggerPro.Repositories
{
    public class CommunityRepository
    {
        private static readonly string connectionString = "Server = localhost; User ID = root; Database = BloggerDB; Password=loveforall1990#";

        public static IEnumerable<Community> GetAll()
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.Query<Community>("SELECT * FROM Communities");
            }
        }

        public static Community GetById(int id)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<Community>("SELECT * FROM Communities WHERE Id = @Id", new { Id = id });
            }
        }

        public static void Add(Community community)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO Communities (Name) VALUES (@Name)", community);
            }
        }

        public static void JoinCommunity(int userId, int communityId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                connection.Execute("INSERT INTO UserCommunities (UserId, CommunityId) VALUES (@UserId, @CommunityId)",
                    new { UserId = userId, CommunityId = communityId });
            }
        }

        public static bool IsUserInCommunity(int userId, int communityId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                return connection.QuerySingleOrDefault<int>("SELECT COUNT(1) FROM UserCommunities WHERE UserId = @UserId AND CommunityId = @CommunityId",
                    new { UserId = userId, CommunityId = communityId }) > 0;
            }
        }
        public static IEnumerable<Community> GetCommunitiesForUser(int userId)
        {
            var communities = new List<Community>();

            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                var sql = @"
            SELECT c.Id, c.Name
            FROM Communities c
            INNER JOIN UserCommunity uc ON c.Id = uc.CommunityId
            WHERE uc.UserId = @UserId";

                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            communities.Add(new Community
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name")
                            });
                        }
                    }
                }
            }

            return communities;
        }
    }
}

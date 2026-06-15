using Dapper;
using TaskboardAPI.Data;
using TaskboardAPI.Models;

namespace TaskboardAPI.Repositories
{
    public class UserRepository
    {
        private readonly DbConnectionFactory _factory;
        public UserRepository(DbConnectionFactory factory)
        {
            _factory = factory;
        }
        public async Task<Users?> GetUserByEmail(string email)
        {

                using var connection = _factory.CreateConnection();
                string query = "SELECT * from Users where Email=@Email";
                var result = await connection.QueryFirstOrDefaultAsync<Users>(
                    query,
                    new
                    {
                        Email = email
                    });
            
            return result;

        }

        public async Task<int> CreateUser(Users user)
        {
            
                using var connection = _factory.CreateConnection();
                string query = @"INSERT INTO Users (UserName,Email,PasswordHash,IsActive,CreatedDate) VALUES(@UserName,@Email,@PasswordHash,@IsActive,GETDATE());
                            SELECT CAST(SCOPE_IDENTITY() as int )";

                var result = await connection.ExecuteScalarAsync<int>(query, user);
         
            return result;

        }
    }
}
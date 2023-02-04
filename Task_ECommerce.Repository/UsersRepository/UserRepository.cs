using System.Data.SqlClient;
using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Repository.UsersRepository
{
    /// <summary>
    /// Implementation of users repository
    /// </summary>
    public class UserRepository : IUserRepository
    {
        #region private fields
        private readonly string _connectionString;
        #endregion

        #region ctor
        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion


        #region public methods 
        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns>Registered user</returns>
        public async Task<User> RegisterAsync(string userName, string password, string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Users (UserName, Email, PasswordHash, CreatedAt, UpdatedAt) VALUES (@userName, @email, @passwordHash, @createdAt, @updatedAt); SELECT CAST(SCOPE_IDENTITY() as int)", connection))
                {
                    command.Parameters.AddWithValue("@userName", userName);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@passwordHash", password);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);

                    await command.ExecuteScalarAsync();

                    return await GetByUserNameAsync(userName);
                }
            }
        }

        /// <summary>
        /// Gets user by its Username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User</returns>
        public async Task<User> GetByUserNameAsync(string userName)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT * FROM Users WHERE UserName = @userName", connection))
                {
                    command.Parameters.AddWithValue("@userName", userName);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                Id = (int)reader["Id"],
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                            };

                            return user;
                        }

                        return null;
                    }
                }
            }
        }
    }
    #endregion
}

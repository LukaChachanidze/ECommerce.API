using System.Data.SqlClient;
using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Repository.CartsRepository
{
    /// <summary>
    /// Implementation of Cart Repository
    /// </summary>
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Creates new cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task CreateCartAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("INSERT INTO Cart (UserId) VALUES (@userId); SELECT SCOPE_IDENTITY()", connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);

                    var result = await command.ExecuteScalarAsync();
                
                }
            }
        }


        /// <summary>
        /// Gets cart by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            Cart? cart = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM Cart WHERE UserId = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            cart = new Cart
                            {
                                Id = (int)reader["Id"],
                            };
                        }
                    }
                }
            }

            return cart;
        }

        /// <summary>
        /// Gets cart items by cart id
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId)
        {
            var cartItems = new List<CartItem>();
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("SELECT * FROM CartItem WHERE CartId = @CartId", connection))
                {
                    command.Parameters.AddWithValue("@CartId", cartId);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cartItem = new CartItem
                            {
                                Id = (int)reader["Id"],
                                ProductId = (int)reader["ProductId"],
                                Quantity = (int)reader["Quantity"],
                            };
                            cartItems.Add(cartItem);
                        }
                    }
                }
            }

            return cartItems;
        }

        /// <summary>
        /// Adds products to cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task AddProductToCartAsync(int userId, int productId, int quantity, int? cartId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var transaction = connection.BeginTransaction())
                {
                     cartId = await GetCartIdForUserAsync(userId, connection, transaction);

                    if (cartId == null)
                    {
                        cartId = await CreateCartForUserAsync(userId, connection, transaction);
                    }

                    using (var command = new SqlCommand("INSERT INTO CartItem (CartId, ProductId, Quantity) VALUES (@CartId, @ProductId, @Quantity)", connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.AddWithValue("@CartId", cartId);
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        await command.ExecuteNonQueryAsync();
                    }

                    transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Removes product from a cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task RemoveProductFromCartAsync(int userId, int productId, int cartId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"DELETE FROM [dbo].[CartItem]
                                WHERE CartId = @CartId
                                AND ProductId = @ProductId";
                    command.Parameters.AddWithValue("@CartId", cartId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ProductId", productId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        /// <summary>
        /// Deletes cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task DeleteCartAsync(int cartId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand("DELETE FROM Cart WHERE Id = @cartId", connection))
                {
                    command.Parameters.AddWithValue("@cartId", cartId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        #region private methods
        private async Task<int?> GetCartIdForUserAsync(int userId, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("SELECT Id FROM Cart WHERE UserId = @UserId", connection))
            {
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@UserId", userId);
                var result = await command.ExecuteScalarAsync();
                if (result != null)
                {
                    return (int)result;
                }
                return null;
            }
        }

        private async Task<int> CreateCartForUserAsync(int userId, SqlConnection connection, SqlTransaction transaction)
        {
            using (var command = new SqlCommand("INSERT INTO Cart (UserId) VALUES (@UserId); SELECT SCOPE_IDENTITY()", connection))
            {
                command.Transaction = transaction;
                command.Parameters.AddWithValue("@UserId", userId);
                return (int)await command.ExecuteScalarAsync();
            }
        }
      #endregion
    }
}



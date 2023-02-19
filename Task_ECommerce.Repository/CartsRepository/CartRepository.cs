using System.Data;
using System.Data.SqlClient;
using System.Transactions;
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

                using (var command = new SqlCommand("CreateCart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    await command.ExecuteNonQueryAsync();
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
                using (var command = new SqlCommand("GetCartByUserId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
                using (var command = new SqlCommand("GetCartItemsByCartId", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
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
                                Product = new Product() {
                                    Name = (string)reader["Name"],
                                    Description = (string)reader["Description"],
                                    Price = (decimal)reader["Price"]
                                }
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
        public async Task AddProductToCartAsync(int userId, int productId, int quantity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                    int? cartId = await GetCartIdForUserAsync(userId, connection);

                    if (cartId == null)
                    {
                        cartId = await CreateCartForUserAsync(userId, connection);
                    }

                    using (var command = new SqlCommand("AddProductToCart", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ProductId", productId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@CartId", cartId);
                        await command.ExecuteNonQueryAsync();
                    }

                await connection.CloseAsync();
            }
        }

        /// <summary>
        /// Removes product from a cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task RemoveProductFromCartAsync(int userId, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                int? cartId = await GetCartIdForUserAsync(userId, connection);

                using (var command = new SqlCommand("RemoveProductFromCart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CartId", cartId);
                    command.Parameters.AddWithValue("@UserId", userId);
                    command.Parameters.AddWithValue("@ProductId", id);
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
                await connection.OpenAsync();

                using (var command = new SqlCommand("dbo.DeleteCart", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cartId", cartId);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        #region private methods
        private async Task<int?> GetCartIdForUserAsync(int userId, SqlConnection connection)
        {
            using (var command = new SqlCommand("GetCartIdForUser", connection))
            {           
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                var result = await command.ExecuteScalarAsync();

                if (result != null)
                {
                    return (int)result;
                }

                return null;
            }
        }


        private async Task<int> CreateCartForUserAsync(int userId, SqlConnection connection)
        {
            using (var command = new SqlCommand("CreateCartForUser", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                return  (int)await command.ExecuteScalarAsync();       
            }
        }
        #endregion
    }
}



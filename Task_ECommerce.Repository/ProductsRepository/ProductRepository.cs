using System.Data;
using System.Data.SqlClient;
using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Repository.ProductsRepository
{
    /// <summary>
    /// Implementation of product repository
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        #region private fields
        private readonly string _connectionString;
        #endregion

        #region ctor
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        #endregion

        #region public methods 
        public async Task<Product> GetByIdAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetProductById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            var product = new Product();
                            product.Id = (int)reader["Id"];
                            product.Name = (string)reader["Name"];
                            product.Description = (string)reader["Description"];
                            product.Price = (decimal)reader["Price"];
                            return product;
                        }

                        return new Product();
                    }
                }
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("GetAllProducts", connection))
                {
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        var products = new List<Product>();
                        while (await reader.ReadAsync())
                        {
                            var product = new Product();
                            product.Id = (int)reader["Id"];
                            product.Name = (string)reader["Name"];
                            product.Description = (string)reader["Description"];
                            product.Price = (decimal)reader["Price"];
                            products.Add(product);
                        }

                        return products;
                    }
                }
            }
        }

        public async Task<int> InsertAsync(Product entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("InsertProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@description", entity.Description);
                    command.Parameters.AddWithValue("@price", entity.Price);
                    command.Parameters.AddWithValue("@createdAt", DateTime.Now);
                    command.Parameters.AddWithValue("@updatedAt", DateTime.Now);
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }


        public async Task<int> UpdateAsync(Product entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UpdateProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", entity.Name);
                    command.Parameters.AddWithValue("@Description", entity.Description);
                    command.Parameters.AddWithValue("@Id", entity.Id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<int> DeleteAsync(Product entity)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("DeleteProduct", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Id", entity.Id);

                    return await command.ExecuteNonQueryAsync();
                }
            }
        }
        #endregion
    }
}
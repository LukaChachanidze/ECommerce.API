using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Repository.ProductsRepository
{
    /// <summary>
    /// Interface for product repository
    /// </summary>
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<int> InsertAsync(Product entity);
        Task<int> UpdateAsync(Product entity);
        Task<int> DeleteAsync(Product entity);
    }
}

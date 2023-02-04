using Task_ECommerce.Services.Products.DTO;

namespace Task_ECommerce.Services.Products
{
    /// <summary>
    /// Interface for product service
    /// </summary>
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<int> InsertProductAsync(CreateProductDTO product);
        Task<int> UpdateProductAsync(UpdateProductDTO product);
        Task<int> DeleteProductAsync(int id);
    }
}

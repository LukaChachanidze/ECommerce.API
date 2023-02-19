using Task_ECommerce.Services.Cart.DTO;

namespace Task_ECommerce.Services.Cart
{
    /// <summary>
    /// Interface for Cart Service
    /// </summary>
    public interface ICartService
    {
        Task<IEnumerable<CartDTO>> GetCartByUserIdAsync(int userId);
        Task AddProductToCartAsync(int userId, int productId, int quantity);
        Task RemoveProductFromCartAsync(int userId, int id);
        Task DeleteCartAsync(int cartId);
        Task CreateCartAsync(int userId);
    }
}

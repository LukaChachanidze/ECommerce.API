using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Repository.CartsRepository
{
    /// <summary>
    /// Interface for cart repository
    /// </summary>
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(int cartId);
        Task AddProductToCartAsync(int userId, int productId, int quantity, int? cartId);
        Task RemoveProductFromCartAsync(int userId, int productId, int cartId);
        Task DeleteCartAsync(int cartId);
        Task CreateCartAsync(int userId);
    }
}

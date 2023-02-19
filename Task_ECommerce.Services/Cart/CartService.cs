using Task_ECommerce.Repository.CartsRepository;
using Task_ECommerce.Services.Cart.DTO;

namespace Task_ECommerce.Services.Cart
{
    /// <summary>
    /// Service for managing cart operations
    /// </summary>
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        
        /// <summary>
        /// Creates new cart
        /// </summary>
        /// <param name="userId"></param>
        public async Task CreateCartAsync(int userId)
        {
            await _cartRepository.CreateCartAsync(userId);
        }

        /// <summary>
        /// Gets Cart Items by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CartDTO>> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                return Enumerable.Empty<CartDTO>();
            }

            var cartItems = await _cartRepository.GetCartItemsByCartIdAsync(cart.Id);

            return cartItems.Select(x => new CartDTO
            {
                Id = x.Id,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Name = x.Product.Name,
                Price = x.Product.Price,
            });
        }

        /// <summary>
        /// Adds product to cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <param name="quantity"></param>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task AddProductToCartAsync(int userId, int productId, int quantity)
        {
            await _cartRepository.AddProductToCartAsync(userId, productId, quantity);
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
            await _cartRepository.RemoveProductFromCartAsync(userId, id);
        }

        /// <summary>
        /// Deletes cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        public async Task DeleteCartAsync(int cartId)
        {
            await _cartRepository.DeleteCartAsync(cartId);
        }
    }
}


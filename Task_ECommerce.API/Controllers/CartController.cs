using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_ECommerce.Services.Cart;
using Task_ECommerce.Services.Cart.DTO;

namespace Task_ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartController : ControllerBase
    {
        #region private fields
        private readonly ICartService _cartService;
        #endregion

        #region ctor
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        #endregion

        #region action methods
        /// <summary>
        /// Method to create a new cart
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("{userId}")]
        public async Task<IActionResult> CreateCartAsync(int userId)
        {
            await _cartService.CreateCartAsync(userId);
            return Ok("Successfully created cart");
        }

        /// <summary>
        /// method to get cart by user Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartByUserIdAsync(int userId)
        {
            var cart = await _cartService.GetCartByUserIdAsync(userId);
            return Ok(cart);
        }

        /// <summary>
        /// Method to Add Product To Cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="cartId"></param>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost("{userId}/{cartId}/products")]
        public async Task<IActionResult> AddProductToCartAsync(int userId, int cartId, [FromBody] CartProductDTO product)
        {
            await _cartService.AddProductToCartAsync(userId, product.ProductId, product.Quantity, cartId);
            return Ok("Successfully added");
        }

        /// <summary>
        /// Method to delete product from a cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{cartId}/products/{productId}")]
        public async Task<IActionResult> RemoveProductFromCartAsync(int userId, int productId, int cartId)
        {
            await _cartService.RemoveProductFromCartAsync(userId, productId, cartId);
            return Ok("Successfully removed");
        }

        /// <summary>
        /// Method to delete cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpDelete("{cartId}")]
        public async Task<IActionResult> DeleteCartAsync(int cartId)
        {
            await _cartService.DeleteCartAsync(cartId);
            return Ok("Successfully deleted");
        }
    }
    #endregion
}

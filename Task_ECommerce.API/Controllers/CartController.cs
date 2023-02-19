using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_ECommerce.API.Models.Carts.Responses;
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
        [HttpPost("{userId}/products")]
        public async Task<IActionResult> AddProductToCartAsync(int userId, [FromBody] CartProductDTO product)
        {
            await _cartService.AddProductToCartAsync(userId, product.Id, product.Quantity);
            return Ok(new CartResponse { IsSuccess = true, Description = "Successfully added product to cart"});
        }

        /// <summary>
        /// Method to delete product from a cart
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpPost("{userId}/{id}")]
        public async Task<IActionResult> RemoveProductFromCartAsync(int userId, int id)
        {
            await _cartService.RemoveProductFromCartAsync(userId, id);
            return Ok(new CartResponse { IsSuccess = true, Description = "Successfully removed product from cart" });
        }

        /// <summary>
        /// Method to delete cart
        /// </summary>
        /// <param name="cartId"></param>
        /// <returns></returns>
        [HttpPost("{cartId}")]
        public async Task<IActionResult> DeleteCartAsync(int cartId)
        {
            await _cartService.DeleteCartAsync(cartId);
            return Ok(new CartResponse {  IsSuccess = true, Description = "Successfully deleted"});
        }
    }
    #endregion
}

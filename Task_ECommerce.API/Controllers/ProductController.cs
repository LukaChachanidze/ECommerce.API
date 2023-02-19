using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_ECommerce.API.Models.Products.Responses;
using Task_ECommerce.Services.Products;
using Task_ECommerce.Services.Products.DTO;

namespace Task_ECommerce.API.Controllers
{
    /// <summary>
    /// Products controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        #region private fields 
        private readonly IProductService _productService;
        #endregion

        #region ctor
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion

        #region action methods
        /// <summary>
        /// Method to get all the products
        /// </summary>
        /// <returns>All the products</returns>
        [HttpGet(nameof(GetAllProductsAsync))]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        /// <summary>
        /// Method to get proudct by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single product</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        /// <summary>
        /// Method to create new product
        /// </summary>
        /// <param name="createProductDto"></param>
        [HttpPost]
        public async Task<IActionResult> InsertProductAsync(CreateProductDTO createProductDto)
        {
            await _productService.InsertProductAsync(createProductDto);
            return Ok(new ProductResponse { IsSuccess = true, Description = "Successfully created"});
        }

        /// <summary>
        /// Method to update existing product
        /// </summary>
        /// <param name="updateProductDto"></param>
        [HttpPost(nameof(UpdateProductAsync))]
        public async Task<IActionResult> UpdateProductAsync(UpdateProductDTO updateProductDto)
        {
            var product = await _productService.UpdateProductAsync(updateProductDto);
            return Ok(new ProductResponse { IsSuccess = true, Description = "Successfully Updated" });
        }

        /// <summary>
        /// Method to delete product by its id
        /// </summary>
        /// <param name="id"></param>
        [HttpPost("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new ProductResponse { IsSuccess = true, Description = "Successfully deleted" });
        }
        #endregion
    }
}

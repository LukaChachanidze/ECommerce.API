using Task_ECommerce.Domain.Entities;
using Task_ECommerce.Repository.ProductsRepository;
using Task_ECommerce.Services.Products.DTO;

namespace Task_ECommerce.Services.Products
{
    /// <summary>
    /// Product service for product entity
    /// </summary>
    public  class ProductService : IProductService
    {
        #region private fields
        private readonly IProductRepository _productRepository;
        #endregion

        #region ctor
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        #endregion

        #region public methods

        /// <summary>
        /// Method to get a single product by its Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A single product></returns>
        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product is not null)
                {
                    return new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price
                    };
                }
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }

            throw new Exception("Couldn't find any product");
        }

        /// <summary>
        /// Gets all the products
        /// </summary>
        /// <returns>IEnumerable of products</returns>
        public async Task<IEnumerable<ProductDTO>> GetAllProductsAsync()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                if (products is not null)
                {
                    return products.Select(x => new ProductDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                    });
                }
            }
            catch (Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }

            throw new Exception("Couldn't find any products");
        }

        /// <summary>
        /// Updates the product
        /// </summary>
        /// <param name="productDto"></param>
        /// <exception cref="Exception"></exception>
        public async Task<int> UpdateProductAsync(UpdateProductDTO productDto)
        {
            try
            {
                if (productDto is not null)
                {
                    var product = new Product
                    {
                        Id = productDto.Id,
                        Name = productDto.Name,
                        Description = productDto.Description,
                        Price = productDto.Price,
                    };
                    return await _productRepository.UpdateAsync(product);
                }
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }

            throw new Exception("Request was empty");
        }

        /// <summary>
        /// Creates a new product
        /// </summary>
        /// <param name="productDto"></param>
        /// <exception cref="Exception"></exception>
        public async Task<int> InsertProductAsync(CreateProductDTO productDto)
        {
            if (productDto is not null)
            {
                var product = new Product
                {
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    
                };
                try
                {
                    return await _productRepository.InsertAsync(product);
                }
                catch(Exception ex)
                {
                    throw;
                    //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
                }
            }

            throw new Exception("Request was empty");
        }

        /// <summary>
        /// Deletes a product
        /// </summary>
        /// <param name="id"></param>
        public async Task<int> DeleteProductAsync(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                return await _productRepository.DeleteAsync(product);
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }
        }
        #endregion
    }
}

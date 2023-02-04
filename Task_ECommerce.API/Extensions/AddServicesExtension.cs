using Task_ECommerce.Repository.CartsRepository;
using Task_ECommerce.Repository.ProductsRepository;
using Task_ECommerce.Repository.UsersRepository;
using Task_ECommerce.Services.Cart;
using Task_ECommerce.Services.Encryption;
using Task_ECommerce.Services.Jwt;
using Task_ECommerce.Services.Products;
using Task_ECommerce.Services.Users;

namespace Task_ECommerce.API.Extensions
{
    /// <summary>
    /// Extension class to add services in startup
    /// </summary>
    public static class AddServicesExtension
    {
        /// <summary>
        /// Extension method to register services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration.GetConnectionString("ECommerceConnection"));
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<ICartRepository, CartRepository>();
        }
    }
}

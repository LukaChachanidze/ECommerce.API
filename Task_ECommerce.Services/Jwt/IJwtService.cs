using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Services.Jwt
{
    /// <summary>
    /// Interface for JWT service
    /// </summary>
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}

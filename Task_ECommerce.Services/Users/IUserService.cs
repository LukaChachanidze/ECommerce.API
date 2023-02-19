using Task_ECommerce.Domain.Models.Users.Responses;
using Task_ECommerce.Services.Users.DTO;

namespace Task_ECommerce.Services.Users
{
    /// <summary>
    /// Interface for users service
    /// </summary>
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(string userName, string password);
        Task<UserForRegistrationDto> RegisterAsync(string userName, string password, string email);
    }
}

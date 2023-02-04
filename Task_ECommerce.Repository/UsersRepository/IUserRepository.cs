using Task_ECommerce.Domain.Entities;

namespace Task_ECommerce.Repository.UsersRepository
{
    /// <summary>
    /// Interface for users repository
    /// </summary>
    public  interface IUserRepository
    {
        Task<User> RegisterAsync(string userName, string password, string email);
        Task<User> GetByUserNameAsync(string userName);
    }
}

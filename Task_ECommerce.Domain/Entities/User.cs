using Task_ECommerce.Domain.Entities.Base;

namespace Task_ECommerce.Domain.Entities
{
    /// <summary>
    /// Entity for User
    /// </summary>
    public class User : BaseEntity<int>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
    }
}

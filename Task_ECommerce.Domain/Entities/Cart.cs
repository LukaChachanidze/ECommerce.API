using Task_ECommerce.Domain.Entities.Base;

namespace Task_ECommerce.Domain.Entities
{
    /// <summary>
    /// Entity for Cart
    /// </summary>
    public class Cart : BaseEntity<int>
    {
        /// <summary>
        /// Foreign Key on User Table
        /// </summary>
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }
    }
}

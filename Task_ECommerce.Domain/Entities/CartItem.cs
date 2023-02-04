using Task_ECommerce.Domain.Entities.Base;

namespace Task_ECommerce.Domain.Entities
{
    /// <summary>
    /// Entity for cart item
    /// </summary>
    public class CartItem : BaseEntity<int>
    {
        /// <summary>
        /// Foreign key on product table
        /// </summary>
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}

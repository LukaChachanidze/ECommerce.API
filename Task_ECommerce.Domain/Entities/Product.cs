using Task_ECommerce.Domain.Entities.Base;

namespace Task_ECommerce.Domain.Entities
{
    /// <summary>
    /// Entity for product
    /// </summary>
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

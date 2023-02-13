namespace Task_ECommerce.Services.Products.DTO
{
    /// <summary>
    /// Data transfer object to get products
    /// </summary>
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}

namespace Task_ECommerce.Services.Products.DTO
{
    /// <summary>
    /// Data transfer object to create a new product
    /// </summary>
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

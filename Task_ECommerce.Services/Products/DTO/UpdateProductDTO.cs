namespace Task_ECommerce.Services.Products.DTO
{
    /// <summary>
    /// Data transfer Object for updating existing product
    /// </summary>
    public class UpdateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
